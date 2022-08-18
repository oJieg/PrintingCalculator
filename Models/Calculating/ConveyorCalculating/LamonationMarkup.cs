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

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result)
        {
            if (history.Input.Lamination == null)
            {
                result.LaminationResult.ActualMarkup = true;
                return (history, result, true);
            }

            CalculatingMarkup markups = new(_markup.MarkupList);
            int Markup = (int)markups.GetMarkup(result.PaperResult.Sheets);

            if (history.LaminationMarkup == null)
            {
                try
                {
                    result.LaminationResult.Markup = Markup;
                    result.LaminationResult.ActualMarkup = true;
                    history.LaminationMarkup = Markup;
                    return (history, result, true);
                }
                catch
                {
                    return (history, result, false);
                }
            }
            else
            {
                result.LaminationResult.Markup = (int)history.LaminationMarkup;
                result.LaminationResult.ActualMarkup = ActualMarkup(history, Markup);
                return (history, result, true);
            }
        }

        private bool ActualMarkup(History history, int markup)
        {
            if (history.LaminationMarkup == markup)
            {
                return true;
            }
            return false;
        }
    }
}