using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class AllPrice : IConveyor
    {
        public  Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if(cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }    

            try
            {
                int Price = Convert.ToInt32(result.PaperResult.Price
                                      + result.LaminationResult.Price
                                      + result.PosResult.CreasingPrice
                                      + result.PosResult.DrillingPrice
                                      + result.PosResult.RoundingPrice);
                result.Price = Convert.ToInt32(Math.Round((double)Price / 10, 1) * 10); //округление

                if (history.Price == null)
                {
                    result.TryPrice = true;
                    history.Price = result.Price;

                }

                if (result.PaperResult.ActualConsumablePrice &&
                    result.PaperResult.ActualMarkupPaper &&
                    result.PaperResult.ActualCostPrise &&
                    result.PaperResult.ActualCutPrice &&
                    result.LaminationResult.ActualCostPrics &&
                    result.LaminationResult.ActualMarkup &&
                    result.PosResult.ActualRoundingPrice &&
                    result.PosResult.ActualCreasingPrice &&
                    result.PosResult.ActualDrillingPrice)
                {
                    result.TryPrice = true;
                }
                else
                {
                    result.TryPrice = false;
                }

                return Task.FromResult((history, result, true));
            }
            catch(OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}