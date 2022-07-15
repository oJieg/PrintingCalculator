using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationMarkup : IConveyor
    {
        private Settings.Lamination _markup;
        public LamonationMarkup(Settings.Lamination markup)
        {
            _markup = markup;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if(history.Input.Lamination == null)
            {
                return true;
            }
            if(history.LaminationMarkup == null)
            {
                CalculatingMarkup markup = new(_markup.MarkupList);
                result.ResultLamination.Markup = (int)markup.GetMarkup(result.ResultPaper.Sheets);
                history.LaminationMarkup = result.ResultLamination.Markup;
                return true;
            }
            else
            {
                result.ResultLamination.Markup = (int)history.LaminationMarkup;
                return true;
            }
        }
    }
}
