using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperInfo : IConveyor
    {
        public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
				return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusAnswer.Cancellation
				}));
			}

            result.PaperResult.NamePaper = history.Input.Paper.Name;
            result.PaperResult.Duplex = history.Input.Duplex;

            return Task.FromResult((history, result, new StatusCalculation()));
        }
    }
}