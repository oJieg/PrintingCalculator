using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class AllPrice : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
               int Price = (int)(result.PaperResult.Price
                                     + result.LaminationResult.Price
                                     + result.PosResult.CreasingPrice
                                     + result.PosResult.DrillingPrice
                                     + result.PosResult.RoundingPrice);
                result.Price = (int)Math.Round((double)Price / 10, 1) * 10; //округеление

                if (history.Price == null)
                {
                    result.TryTrice = true;
                    history.Price = result.Price;

                }

                if (result.PaperResult.ActualConsumablePrice &&
                    result.PaperResult.ActualMarkupPaper &&
                    result.PaperResult.ActualCostPrise &&
                    result.PaperResult.ActualCutPrics &&
                    result.LaminationResult.ActualCostPrics &&
                    result.LaminationResult.ActualMarkup &&
                    result.PosResult.ActualRoundingPrice &&
                    result.PosResult.ActualCreasingPrice &&
                    result.PosResult.ActualDrillingPrice)
                {
                    result.TryTrice = true;
                }
                else
                {
                    result.TryTrice = false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}