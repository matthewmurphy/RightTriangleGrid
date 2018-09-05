using System;

namespace RightTriangleGrid.Models
{
    public struct Vertex
    {
        public int X { get; }
        public int Y { get; }

        public Vertex(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vertex(string coordinates)
        {
            try {
                string[] split = coordinates.Split(',');

                this.X = int.Parse(split[0]);
                this.Y = int.Parse(split[1]);
            }
            catch
            {
                throw new FormatException($"Unable to parse coordinate values from '{coordinates}'. Must be 2 integers separated by a comma");
            }
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}