using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        private Setting _settings;
        public PaperCostPrice(Setting settings)
        {
            _settings = settings;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.ResultPaper.CostPrise = (int)(result.ResultPaper.Sheets 
                * (history.PricePaper.Price + result.ResultPaper.ConsumablePrice)); 
            return true;
        }
    }
}
