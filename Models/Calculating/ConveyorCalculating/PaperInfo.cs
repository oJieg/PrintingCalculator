using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperInfo : IConveyor
    {
        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            result.PaperResult.NamePaper = history.Input.Paper.Name;
            result.PaperResult.Duplex = history.Input.Duplex;

            return Task.FromResult((history, result, true));
        }
    }
}