using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class AllPrice : IConveyor
    {
        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            try
            {
                result.Price = Convert.ToInt32(Math.Round((double)result.Price / 100, 1) * 100); //округление

                if (history.Price == null)
                {
                    result.TryPrice = true;
                    history.Price = result.Price;
                }

                bool actualPaperPrice = result.PaperResult.ActualConsumablePrice &&
                    result.PaperResult.ActualMarkupPaper &&
                    result.PaperResult.ActualCostPrise &&
                    result.PaperResult.ActualCutPrice;

                bool actualLaminationPrice = result.LaminationResult.ActualCostPrics &&
                    result.LaminationResult.ActualMarkup;

                bool actualPosPrice = result.PosResult.ActualRoundingPrice &&
                    result.PosResult.ActualCreasingPrice &&
                    result.PosResult.ActualDrillingPrice;

                result.TryPrice = actualPaperPrice &&
                    actualLaminationPrice &&
                    actualPosPrice &&
                    result.Price == history.Price;

                if (result.Price != history.Price)
                {
                    result.Price = history.Price.Value;
                    result.TryPrice = false;
                }

                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}