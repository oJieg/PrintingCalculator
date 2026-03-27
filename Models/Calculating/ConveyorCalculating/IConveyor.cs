using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.ViewModels;

namespace printing_calculator.Models.ConveyorCalculating
{
    public interface IConveyor
    {
        public Task<(СalculationHistory, CalculationResult, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, CalculationResult result, CancellationToken cancellationToken);
    }
}