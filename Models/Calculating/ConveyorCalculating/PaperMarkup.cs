using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperMarkup : IConveyor
    {
        private readonly Setting _settings;

        public PaperMarkup(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            CalculatingMarkup markup = new(_settings.PrintingsMachines[0].Markups);
            int markupPaper = markup.GetMarkup(result.PaperResult.Sheets);

            if (history.MarkupPaper == null)
            {
                result.PaperResult.MarkupPaper = markupPaper;
                result.PaperResult.ActualMarkupPaper = true;
                history.MarkupPaper = markupPaper;
            }
            else
            {
                result.PaperResult.MarkupPaper = (int)history.MarkupPaper;
                result.PaperResult.ActualMarkupPaper = history.MarkupPaper == markupPaper;
            }
            return Task.FromResult((history, result, true));
        }
    }
}