namespace Lab6_MathBalance.Models
{
    public class PotokInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public double Error { get; set; }
        public bool IsUsed { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
        public int SourceId { get; set; }
        public int DestId { get; set; }
    }
}
