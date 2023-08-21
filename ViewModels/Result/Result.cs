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
        public List<string> CommonToAllMarkupName { get; set; }
		public List<bool> TryCommonToAllMarkup { get; set; }

        public DateTime DateTime { get; set; }
        public string? Comment { get; set; }

		public bool IsActualPaperPrice()
        {
            bool actualPaperPrice = PaperResult.ActualConsumablePrice &&
                PaperResult.ActualMarkupPaper &&
                PaperResult.ActualCostPrise &&
                PaperResult.ActualCutPrice;

            bool actualLaminationPrice = LaminationResult.ActualCostPrics &&
                LaminationResult.ActualMarkup;

            bool actualPosPrice = PosResult.ActualRoundingPrice &&
                PosResult.ActualCreasingPrice &&
                PosResult.ActualDrillingPrice;
            return actualPaperPrice &&
                    actualLaminationPrice &&
                    actualPosPrice;
        }
    }
}