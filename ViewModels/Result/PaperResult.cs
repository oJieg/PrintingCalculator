namespace printing_calculator.ViewModels.Result
{
    public class PaperResult
    {
        public string? NamePaper { get; set; }
        public bool Duplex { get; set; }
        public int Sheets { get; set; } //листов необхзодимо
        public int? PiecesPerSheet { get; set; } //штук на листе
        public float ConsumablePrice { get; set; }
        public bool ActualConsumablePrice { get; set; }
        public int? CostPrise { get; set; }
        public bool ActualCostPrise { get; set; }
        public int? MarkupPaper { get; set; }
        public bool ActualMarkupPaper { get; set; }
        public int? CutPrics { get; set; }
        public bool ActualCutPrics { get; set; }
        public int? Price { get; set; }
    }
}