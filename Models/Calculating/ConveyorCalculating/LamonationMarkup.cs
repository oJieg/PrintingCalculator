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
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.Input.Lamination == null)
            {
                result.LaminationResult.ActualMarkup = true;
                return true;
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
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                result.LaminationResult.Markup = (int)history.LaminationMarkup;
                result.LaminationResult.ActualMarkup = ActualMarkup(history, Markup);
                return true;
            }
        }

        private bool ActualMarkup(History history, int markup)
        {
            if(history.LaminationMarkup == markup)
            {
                return true;
            }
            return false;
        }
    }
}