using System;

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

        private const int defaultScale = 10;
        private int Scale;

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
        /// <param name="v1">Top left</param>
        /// <param name="v2">Bottom left (odd col), top right (even col)</param>
        /// <param name="v3">Bottom right</param>
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
        /// Validates that the vertices can make a valid triangle on the grid
        /// </summary>
        /// <returns>True if triangle is valid</returns>
        public bool ValidateVertices()
        {
            foreach (Vertex v in Vertices)
            {
                if (v.X % Scale != 0 || v.Y % Scale != 0)
                    return false;
            }

            if (V1.X + Scale != V3.X || V1.Y + Scale != V3.Y)
                return false;

            bool oddCol = ColumnIsEven();
            bool evenCol = ColumnIsOdd();

            return oddCol || evenCol;
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

            Column = V1.X / Scale + 1;
            if (ColumnIsEven())
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

        private bool ColumnIsOdd()
        {
            return V1.X == V2.X && V1.Y + Scale == V2.Y;
        }

        private bool ColumnIsEven()
        {
            return V1.X + Scale == V2.X && V1.Y == V2.Y;
        }

        public override string ToString()
        {
            return $"{ID}: V1{V1}, V2{V2}, V3{V3}";
        }
    }
}