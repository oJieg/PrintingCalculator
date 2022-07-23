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
                return true;
            }
            if (history.LaminationMarkup == null)
            {
                try
                {
                    CalculatingMarkup markup = new(_markup.MarkupList);
                    result.LaminationResult.Markup = (int)markup.GetMarkup(result.PaperResult.Sheets);
                    history.LaminationMarkup = result.LaminationResult.Markup;
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
                return true;
            }
        }
    }
}