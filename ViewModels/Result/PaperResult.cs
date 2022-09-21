namespace printing_calculator.ViewModels.Result
{
    public class PaperResult
    {
        public string? NamePaper { get; set; }
        public bool Duplex { get; set; }
        /// <summary>
        /// листов необходимо для печати
        /// </summary>
        public int Sheets { get; set; } 
        /// <summary>
        /// штук на листе
        /// </summary>
        public int PiecesPerSheet { get; set; }
        public float ConsumablePrinterPrice { get; set; }
        public bool ActualConsumablePrice { get; set; }
        public int CostConsumablePrise { get; set; }
        public bool ActualCostPrise { get; set; }
        public int MarkupPaper { get; set; }
        public bool ActualMarkupPaper { get; set; }
        public int CutPrics { get; set; }
        public bool ActualCutPrice { get; set; }
        public int Price { get; set; }
    }
}