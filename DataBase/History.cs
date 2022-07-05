namespace printing_calculator.DataBase
{
    public class History
    {
        public int Id { get; set; }
        public HistoryInput Input { get; set; }
        public PricePaper PricePaper { get; set; }
        public ConsumablePrice ConsumablePrice { get; set; }
        public Markup Markup { get; set; }
    }
}
