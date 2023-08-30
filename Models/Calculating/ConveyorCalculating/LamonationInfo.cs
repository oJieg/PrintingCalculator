using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationInfo : IConveyor
    {
        public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
				return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusType.Cancellation
				}));
			}

            if (history.Input.Lamination != null)
            {
                result.LaminationResult.Name = history.Input.Lamination.Name;
            }
            return Task.FromResult((history, result, new StatusCalculation()));
        }
    }
}