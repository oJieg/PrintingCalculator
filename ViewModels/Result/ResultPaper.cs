namespace printing_calculator.ViewModels.Result
{
    public class ResultPaper
    {
        public string? NamePaper { get; set; }
        public bool Duplex { get; set; }
        public int Sheets { get; set; } //листов необхзодимо
        public int? PiecesPerSheet { get; set; } //штук на листе
        public float  ConsumablePrice { get; set; }
        public int? CostPrise { get; set; }
        public int? MarkupPaper { get; set; }
        public int? CutPrics { get; set; }
        public int? PrisePaper { get; set; }
    }
}
