namespace printing_calculator.DataBase
{
    public class СalculationHistory
    {
        public int Id { get; set; }
        public DateTime dateTime { get; set; }
        public InputHistory Input { get; set; } = null!;
        public float PaperPrice { get; set; }
        public ConsumablePrice ConsumablePrice { get; set; } = null!;
        public int? MarkupPaper { get; set; }
        public int? CutPrice { get; set; }
        public float? LaminationPrices { get; set; }
        public int? LaminationMarkup { get; set; }
        public int? CreasingPrice { get; set; }
        public int? DrillingPrice { get; set; }
        public int? RoundingPrice { get; set; }
        public int? Price { get; set; }
	}
}