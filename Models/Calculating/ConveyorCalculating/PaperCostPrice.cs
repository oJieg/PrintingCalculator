using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        private ApplicationContext _DB;
        public PaperCostPrice(ApplicationContext DB)
        {
            _DB = DB;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                result.PaperResult.ActualCostPrise = ActualData(history);

                result.PaperResult.CostPrise = (int)(result.PaperResult.Sheets
                    * (history.PricePaper.Price + result.PaperResult.ConsumablePrice));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool ActualData(History history)
        {
            int PriceId = history.PricePaper.Id;
            List<PricePaper> ActualPriceId = _DB.PaperCatalogs
                .Where(x => x.Name == history.Input.Paper.Name)
                .First()
                .Prices;

            if (PriceId == ActualPriceId[ActualPriceId.Count - 1].Id)
            {
                return true;
            }
            return false;
        }
    }
}