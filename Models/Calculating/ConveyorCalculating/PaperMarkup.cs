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
                CalculatingMarkup markup = new(_markup.MarkupList);
                int MarkupPaper = (int)markup.GetMarkup(result.PaperResult.Sheets);

                if (history.MarkupPaper == null)
                {
                    result.PaperResult.MarkupPaper = MarkupPaper;
                    result.PaperResult.ActualMarkupPaper = true;
                    history.MarkupPaper = MarkupPaper;

                    return true;
                }
                else
                {
                    result.PaperResult.MarkupPaper = (int)history.MarkupPaper;
                    result.PaperResult.ActualMarkupPaper = ActualMarkup(history, MarkupPaper);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool ActualMarkup(History history, int markup)
        {
            if (history.MarkupPaper == markup)
            {
                return true;
            }
            return false;
        }
    }
}