using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                result.PaperResult.CostPrise = (int)(result.PaperResult.Sheets
                    * (history.PricePaper.Price + result.PaperResult.ConsumablePrice));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}