namespace printing_calculator.ViewModels.Result
{
    public class Result
    {
        public int HistoryInputId { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; }
        public int Height { get; set; }
        public int Whidth { get; set; }
        public ResultPaper? ResultPaper { get; set; }
        public ResultLamination? ResultLamination { get; set; }
        public ResultPos? ResultPos { get; set; }
        public int Prise { get; set; }
    }
}
