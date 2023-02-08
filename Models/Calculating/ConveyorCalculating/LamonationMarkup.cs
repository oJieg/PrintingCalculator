using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationMarkup : IConveyor
    {
        private readonly Setting _settings;

        public LamonationMarkup(Setting settings)
        {
            _settings= settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
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

            CalculatingMarkup markups = new(_settings.Machines[0].Markup);
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