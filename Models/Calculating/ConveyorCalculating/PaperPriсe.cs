using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPriсe : IConveyor
    {
        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return (history, result, false);
            }

            try
            {
                result.PaperResult.Price = (int)(result.PaperResult.CostPrise + (result.PaperResult.CostPrise * (float)result.PaperResult.MarkupPaper / (float)100)) + result.PaperResult.CutPrics;
                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }
    }
}