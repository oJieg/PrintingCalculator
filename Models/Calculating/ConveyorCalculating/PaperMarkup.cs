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

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                if (history.MarkupPaper == null)
                {
                    CalculatingMarkup markup = new(_markup.MarkupList);
                    result.PaperResult.MarkupPaper = (int)markup.GetMarkup(result.PaperResult.Sheets);
                    return true;
                }
                else
                {
                    result.PaperResult.MarkupPaper = (int)history.MarkupPaper;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}