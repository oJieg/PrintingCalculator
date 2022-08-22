using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class Info : IConveyor
    {
        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return (history, result, false);
            }

            result.HistoryInputId = history.Id;
                result.Amount = history.Input.Amount;
                result.Kinds = history.Input.Kinds;
                result.Height = history.Input.Height;
                result.Whidth = history.Input.Whidth;

                return (history, result, true);
        }
    }
}