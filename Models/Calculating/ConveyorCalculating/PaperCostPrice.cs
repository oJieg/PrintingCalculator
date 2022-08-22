using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        private readonly ApplicationContext _applicationContext;

        public PaperCostPrice(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            try
            {
                result.PaperResult.ActualCostPrise = await ActualData(history, cancellationToken);

                result.PaperResult.CostPrise = (int)(result.PaperResult.Sheets
                    * (history.PricePaper.Price + result.PaperResult.ConsumablePrice));
                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }

        private async Task<bool> ActualData(History history, CancellationToken cancellationToken)
        {
            try
            {
                int PriceId = history.PricePaper.Id;
                PaperCatalog ThisPaper = await _applicationContext.PaperCatalogs
                     .AsNoTracking()
                     .Where(x => x.Name == history.Input.Paper.Name)
                     .Include(x => x.Prices)
                     .FirstAsync(cancellationToken);
                List<PricePaper> ActualPriceId = ThisPaper.Prices;

                if (PriceId == ActualPriceId[^1].Id)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}