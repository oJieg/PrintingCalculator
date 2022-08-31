using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationMarkup : IConveyor
    {
        private readonly Settings.Lamination _markup;

        public LamonationMarkup(Settings.Lamination markup)
        {
            _markup = markup;
        }

        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            if (history.Input.Lamination == null)
            {
                result.LaminationResult.ActualMarkup = true;
                return Task.FromResult((history, result, true));
            }

            CalculatingMarkup markups = new(_markup.MarkupList);
            int Markup = (int)markups.GetMarkup(result.PaperResult.Sheets);

            if (history.LaminationMarkup == null)
            {
                    result.LaminationResult.Markup = Markup;
                    result.LaminationResult.ActualMarkup = true;
                    history.LaminationMarkup = Markup;
                    return Task.FromResult((history, result, true));
            }
            else
            {
                result.LaminationResult.Markup = (int)history.LaminationMarkup;
                result.LaminationResult.ActualMarkup = (history.LaminationMarkup == Markup);
                return Task.FromResult((history, result, true));
            }
        }
    }
}