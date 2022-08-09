namespace printing_calculator.DataBase
{
    public class History
    {
        public int Id { get; set; }
        public HistoryInput Input { get; set; }
        public PricePaper PricePaper { get; set; }
        public ConsumablePrice ConsumablePrice { get; set; }
        public int? MarkupPaper { get; set; }
        public int? CutPrice { get; set; }
        public LaminationPrice? LaminationPrices { get; set; }
        public int? LaminationMarkup { get; set; }
        public int? CreasingPrice { get; set; }
        public int? DrillingPrice { get; set; }
        public int? RoundingPrice { get; set; }
        public int? Price { get; set; }
    }
}