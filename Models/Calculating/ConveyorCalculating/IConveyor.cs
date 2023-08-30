using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.ViewModels;

namespace printing_calculator.Models.ConveyorCalculating
{
    public interface IConveyor
    {
        public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken);
    }
}