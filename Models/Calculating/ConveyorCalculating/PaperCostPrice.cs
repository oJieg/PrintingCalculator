using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        private readonly ApplicationContext _applicationContext;
        public PaperCostPrice(ApplicationContext DB)
        {
            _applicationContext = DB;
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
            List<PricePaper> ActualPriceId = _applicationContext.PaperCatalogs
                .Where(x => x.Name == history.Input.Paper.Name)
                .Include(x => x.Prices)
                .First()
               // .AsNoTracking()
                .Prices;

            if (PriceId == ActualPriceId[^1].Id)
            {
                return true;
            }
            return false;
        }
    }
}