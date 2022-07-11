using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.markup;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperMarkup : IConveyor
    {
        private MarkupPaper _markup;
        public PaperMarkup(MarkupPaper markup)
        {
            _markup = markup;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.MarkupPaper == null)
            {
                CalculatingMarkup markup = new(_markup.MarkupList);
                result.ResultPaper.MarkupPaper =(int)markup.GetMarkup(result.ResultPaper.Sheets);
                return true;
            }
            else
            {
                result.ResultPaper.MarkupPaper =(int)history.MarkupPaper;
                return true;
            }
        }
    }
}
