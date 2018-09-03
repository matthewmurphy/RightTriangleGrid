namespace RightTriangleGrid.Models
{
    public struct TriangleLabel
    {
        public char Row { get; set; }
        public int Column { get; set; }

        public TriangleLabel(char row, int col)
        {
            this.Row = row;
            this.Column = col;
        }

        public override string ToString()
        {
            return $"{Row}{Column}";
        }
    }
}