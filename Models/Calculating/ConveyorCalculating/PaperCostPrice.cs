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

                result.PaperResult.CostConsumablePrise = Convert.ToInt32(result.PaperResult.Sheets
                     * (history.PricePaper.Price + result.PaperResult.ConsumablePrinterPrice));
                return (history, result, true);
            }
            catch (OverflowException)
            {
                return (history, result, false);
            }
        }

        private async Task<bool> ActualData(History history, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.PaperCatalogs
                    .AsNoTracking()
                    .Where(paperCatalogs => paperCatalogs.Name == history.Input.Paper.Name)
                    .Select(x => x.Prices.OrderBy(x => x.Id).Last().Id == history.Input.Paper.Id)
                    .FirstAsync(cancellationToken);
            }
            catch
            {
                return false;
            }
        }
    }
}