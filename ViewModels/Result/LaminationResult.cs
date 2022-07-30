namespace printing_calculator.ViewModels.Result
{
    public class LaminationResult
    {
        public string Name { get; set; }
        public int Sheets { get; set; }
        public int CostPrice { get; set; }
        public bool ActualCostPrics { get; set; }
        public int Markup { get; set; }
        public bool ActualMarkup { get; set; }
        public int Price { get; set; }
    }
}