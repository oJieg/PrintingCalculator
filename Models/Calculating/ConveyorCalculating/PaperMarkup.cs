using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperMarkup : IConveyor
    {
        private readonly MarkupPaper _markup;

        public PaperMarkup(MarkupPaper markup)
        {
            _markup = markup;
        }

        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            CalculatingMarkup markup = new(_markup.MarkupList);
            int MarkupPaper = (int)markup.GetMarkup(result.PaperResult.Sheets);

            if (history.MarkupPaper == null)
            {
                result.PaperResult.MarkupPaper = MarkupPaper;
                result.PaperResult.ActualMarkupPaper = true;
                history.MarkupPaper = MarkupPaper;

                return Task.FromResult((history, result, true));
            }
            else
            {
                result.PaperResult.MarkupPaper = (int)history.MarkupPaper;
                result.PaperResult.ActualMarkupPaper = history.MarkupPaper == MarkupPaper;

                return Task.FromResult((history, result, true));
            } 
        }
    }
}