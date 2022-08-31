using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationInfo : IConveyor
    {
        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            result.LaminationResult = new();
            if (history.Input.Lamination != null)
            {
                    result.LaminationResult.Name = history.Input.Lamination.Name;
                    return Task.FromResult((history, result, true));
            }
            return Task.FromResult((history, result, true));
        }
    }
}