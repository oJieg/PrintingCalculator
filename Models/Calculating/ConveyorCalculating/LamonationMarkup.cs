using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationMarkup : IConveyor
    {
        private readonly List<Markup> _markup;

        public LamonationMarkup(Settings.Lamination laminationSetting)
        {
            _markup = laminationSetting.Markups;
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

            CalculatingMarkup markups = new(_markup);
            int markup = markups.GetMarkup(result.PaperResult.Sheets);

            if (history.LaminationMarkup == null)
            {
                result.LaminationResult.Markup = markup;
                result.LaminationResult.ActualMarkup = true;
                history.LaminationMarkup = markup;
            }
            else
            {
                result.LaminationResult.Markup = (int)history.LaminationMarkup;
                result.LaminationResult.ActualMarkup = (history.LaminationMarkup == markup);
            }
            return Task.FromResult((history, result, true));
        }
    }
}