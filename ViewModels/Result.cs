namespace printing_calculator.ViewModels
{
    public class Result
    {
        public int HistoryId { get; set; }
        public string PaperName { get; set; }
        public int Amount { get; set; }
        public int Kinds { get; set; } //видов
        public int Height { get; set; }
        public int Whidth { get; set; }
        public bool Duplex { get; set; }
        public int Sheets { get; set; }
        public int PiecesPerSheet { get; set; }
        public int Price { get; set; }
        public int CostPrice { get; set; }
        public float Markup { get; set; } 
    }
}
