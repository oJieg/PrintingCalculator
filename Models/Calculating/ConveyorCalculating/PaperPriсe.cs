using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPriсe : IConveyor
    {
        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            try
            {
                int? price = Convert.ToInt32(result.PaperResult.CostPrise + (result.PaperResult.CostPrise * (float)result.PaperResult.MarkupPaper / (float)100)) + result.PaperResult.CutPrics;
                if (price == null)
                    return Task.FromResult((history, result, false));

                result.PaperResult.Price = price.Value;
                result.Price += price.Value;
                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}