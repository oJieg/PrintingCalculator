namespace printing_calculator.ViewModels.Result
{
    public class Result
    {
        public int HistoryInputId { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public PaperResult? PaperResult { get; set; }
        public LaminationResult LaminationResult { get; set; }
        public PosResult? PosResult { get; set; }
        public int Price { get; set; }
        public bool TryTrice   { get; set; }
    }
}