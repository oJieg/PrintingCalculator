using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        private readonly ApplicationContext _applicationContext;
        private readonly CancellationToken _cancellationToken;
        public PaperCostPrice(ApplicationContext applicationContext, CancellationToken cancellationToken)
        {
            _applicationContext = applicationContext;
            _cancellationToken = cancellationToken;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result)
        {
            try
            {
                result.PaperResult.ActualCostPrise = await ActualData(history);

                result.PaperResult.CostPrise = (int)(result.PaperResult.Sheets
                    * (history.PricePaper.Price + result.PaperResult.ConsumablePrice));
                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }

        private async Task<bool> ActualData(History history)
        {
            try
            {
                int PriceId = history.PricePaper.Id;
                PaperCatalog ThisPaper = await _applicationContext.PaperCatalogs
                     .AsNoTracking()
                     .Where(x => x.Name == history.Input.Paper.Name)
                     .Include(x => x.Prices)
                     .FirstAsync(_cancellationToken);
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