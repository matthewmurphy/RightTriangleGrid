using System;
using System.Collections.Generic;
using System.Linq;

namespace RightTriangleGrid.Models
{
    public class Triangle
    {
        public string ID { get; private set; }
        public char Row { get; private set; }
        public int Column { get; private set; }

        public Vertex V1 { get; private set; }
        public Vertex V2 { get; private set; }
        public Vertex V3 { get; private set; }
        public Vertex[] Vertices { get; private set; }

        private bool isOddColumn;

        private int Scale;

        private const int defaultScale = 10;

        private const int asciiOffset = 64;

        /// <summary>
        /// Create a triangle from row and column ID values 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="scale"></param>
        public Triangle(char row, int column, int scale = defaultScale)
        {
            this.Row = char.ToUpper(row);
            this.Column = column;
            this.ID = Row.ToString() + Column;
            this.Scale = scale;
        }

        /// <summary>
        /// Create a triangle from 3 vertices
        /// </summary>
        /// <param name="v1">Top-left</param>
        /// <param name="v2">Bottom-left (odd col), top-right (even col)</param>
        /// <param name="v3">Bottom-right</param>
        /// <param name="scale">Side length of non-hypotenus sides</param>
        public Triangle(Vertex v1, Vertex v2, Vertex v3, int scale = defaultScale)
        {
            this.V1 = v1;
            this.V2 = v2;
            this.V3 = v3;
            this.Vertices = new Vertex[] { V1, V2, V3 };
            this.Scale = scale;
        }

        /// <summary>
        /// Based on the vertices, find and set the ID
        /// </summary>
        /// <returns>ID of the triangle</returns>
        public string FindIdFromVertices()
        {
            if (!ValidateVertices())
                throw new InvalidOperationException("Vertices do not create a valid triangle in the grid.");

            Row = (char)(V1.Y / Scale + asciiOffset + 1);

            Column = 2 * V1.X / Scale + 1;
            if (!isOddColumn)
                Column++;

            ID = Row.ToString() + Column;

            return ID;
        }

        /// <summary>
        /// Based on the ID, find and set the vertices
        /// </summary>
        /// <returns></returns>
        public Vertex[] FindVerticesFromId()
        {
            // Convert from A, B, C rows to their corresponding integers
            int rowInt = Row - asciiOffset;

            // Top left corner
            int v1x = (Column - 1) / 2 * Scale;
            int v1y = (rowInt - 1) * Scale;

            // Top right corner for even columns
            // Bottom left corner for odd columns
            int v2x = v1x;
            int v2y = v1y;
            if (Column % 2 == 0)
                v2x += Scale;
            else
                v2y += Scale;

            // Bottom right corner
            int v3x = v1x + Scale;
            int v3y = v1y + Scale;

            V1 = new Vertex(v1x, v1y);
            V2 = new Vertex(v2x, v2y);
            V3 = new Vertex(v3x, v3y);
            Vertices = new Vertex[] { V1, V2, V3 };

            return Vertices;
        }


        /// <summary>
        /// Validates that the vertices can make a valid triangle on the grid
        /// </summary>
        /// <returns>True if triangle is valid</returns>
        public bool ValidateVertices()
        {
            if (Vertices.Any(v =>
                    v.X % Scale != 0
                    || v.Y % Scale != 0))
                return false;

            int minX = Vertices.Min(v => v.X);
            int minY = Vertices.Min(v => v.Y);
            int maxX = Vertices.Max(v => v.X);
            int maxY = Vertices.Max(v => v.Y);

            bool xScaleValid = (minX + Scale == maxX);
            bool yScaleValid = (minY + Scale == maxY);
            if (!(xScaleValid && yScaleValid))
                return false;

            // Two points must have the same X value, two other points must have the same Y value
            var leftSide = Vertices.Where(v => v.X == minX);
            if (leftSide.Count() == 2)
            {
                V1 = leftSide.SingleOrDefault(v => v.Y == minY);
                V2 = leftSide.SingleOrDefault(v => v.Y == maxY);
                V3 = Vertices.Except(new Vertex[] { V1, V2 }).SingleOrDefault();
                isOddColumn = true;
            }
            else
            {
                var rightSide = Vertices.Where(v => v.X == maxX);
                if (rightSide.Count() != 2)
                    return false;

                V2 = rightSide.SingleOrDefault(v => v.Y == minY);
                V3 = rightSide.SingleOrDefault(v => v.Y == maxY);
                V1 = Vertices.Except(new Vertex[] { V2, V3 }).SingleOrDefault();
                isOddColumn = false;
            }

            this.Vertices = new Vertex[] { V1, V2, V3 };

            return Vertices.All(v => v != null);
        }

        public override string ToString()
        {
            return $"{ID}: V1{V1}, V2{V2}, V3{V3}";
        }
    }
}