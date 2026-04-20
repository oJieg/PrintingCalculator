using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCostPrice : IConveyor
    {
        public async Task<(СalculationHistory, CalculationResult, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, CalculationResult result, CancellationToken cancellationToken)
        {
            try
            {
                result.PaperResult.CostConsumablePrise = Convert.ToInt32(result.PaperResult.Sheets
                     * (history.PaperPrice + result.PaperResult.ConsumablePrinterPrice));
                return (history, result, new StatusCalculation());
            }
            catch (OverflowException)
            {
                return (history, result, new StatusCalculation() { 
                    Status = StatusAnswer.Other,
                    ErrorMassage = "Стоимость бумаги вышла за возможные приделы int"
                });
            }
        }
    }
}