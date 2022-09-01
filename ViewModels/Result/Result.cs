namespace printing_calculator.ViewModels.Result
{
    public class Result
    {
        public int HistoryInputId { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public PaperResult PaperResult { get; set; } = new();
        public LaminationResult LaminationResult { get; set; } = new();
        public PosResult PosResult { get; set; } = new();
        public int Price { get; set; }
        public bool TryPrice { get; set; }
    }
}