namespace RightTriangleGrid.Models
{
    public class Triangle
    {
        public string ID { get; set; }

        public Point P1 { get; set; }
        public Point P2 { get; set; }
        public Point P3 { get; set; }

        public override string ToString()
        {
            return $"{ID}: A{P1}, B{P2}, C{P3}";
        }
    }
}