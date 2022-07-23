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
                result.Price = (int)(result.PaperResult.Price
                                     + result.LaminationResult.Price
                                     + result.PosResult.CreasingPrice
                                     + result.PosResult.DrillingPrice
                                     + result.PosResult.RoundingPrice);

                if (history.Price == null)
                {
                    result.TryTrice = true;
                    history.Price = result.Price;

                }

                if (history.Price != result.Price)
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