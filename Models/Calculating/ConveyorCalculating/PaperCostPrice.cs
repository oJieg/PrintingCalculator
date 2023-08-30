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

        public async Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            try
            {
                result.PaperResult.ActualCostPrise = await ActualData(history, cancellationToken);

                result.PaperResult.CostConsumablePrise = Convert.ToInt32(result.PaperResult.Sheets
                     * (history.PaperPrice + result.PaperResult.ConsumablePrinterPrice));
                return (history, result, new StatusCalculation());
            }
            catch (OverflowException)
            {
                return (history, result, new StatusCalculation() { 
                    Status = StatusType.Other,
                    ErrorMassage = "Стоимость расходных материалов вышла за возможные приделы int"
                });
            }
        }

        private async Task<bool> ActualData(СalculationHistory history, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.PaperCatalogs
                    .AsNoTracking()
                    .Where(paperCatalogs => paperCatalogs.Name == history.Input.Paper.Name)
                    .Select(x => x.Prices == history.PaperPrice)
                    .FirstAsync(cancellationToken);
            }
            catch
            {
                return false;
            }
        }
    }
}