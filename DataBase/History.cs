namespace printing_calculator.DataBase
{
    public class History
    {
        public int Id { get; set; }
        public HistoryInput Input { get; set; }
        public PricePaper PricePaper { get; set; }
        public ConsumablePrice ConsumablePrice { get; set; }
        public float MarkupPaper { get; set; }
        public LaminationPrice? LaminationPrices { get; set; }
        public float? LaminationMarkup { get; set; }
        public float CreasingPrice { get; set; }
        public float DrillingPrice { get; set; }
        public float RoundingPrice { get; set; }
    }
}
