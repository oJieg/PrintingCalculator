namespace printing_calculator.ViewModels
{
    public class SimpleResult
    {
        public int HistoryId { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public string PaperName { get; set; } = null!;
        public bool Lamination { get; set; }
        public bool Creasing { get; set; }
        public bool Drilling { get; set; }
        public bool Rounding { get; set; }
        public int Price { get; set; }
        public DateTime DateTime { get; set; }
        public string? Comment { get; set; }
    }
}
