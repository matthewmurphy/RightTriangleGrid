namespace RightTriangleGrid.Models
{
    public class Triangle
    {
        public TriangleLabel Label { get; set; }

        public Point A { get; set; }
        public Point B { get; set; }
        public Point C { get; set; }

        public override string ToString()
        {
            return $"{Label}: A{A}, B{B}, C{C}";
        }
    }
}