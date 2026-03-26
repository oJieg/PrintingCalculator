using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class Info : IConveyor
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

            result.HistoryInputId = history.Id;
            result.Amount = history.Input.Amount;
            result.Kinds = history.Input.Kinds;
            result.Height = history.Input.Height;
            result.Whidth = history.Input.Whidth;
            result.DateTime = history.DateTime;
            result.Comment = history.Comment;


            return Task.FromResult((history, result, new StatusCalculation()));
        }
    }
}