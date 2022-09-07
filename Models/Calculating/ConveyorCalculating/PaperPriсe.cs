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
                int? pricePaperWithMarkup = Convert.ToInt32(result.PaperResult.CostPrise + 
                    (result.PaperResult.CostPrise * (float)result.PaperResult.MarkupPaper / (float)100));
                int? pricePaper = pricePaperWithMarkup + result.PaperResult.CutPrics;
                if (pricePaperWithMarkup == null || pricePaper == null)
                    return Task.FromResult((history, result, false));

                result.PaperResult.Price = pricePaper.Value;
                result.Price += pricePaper.Value;
                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}