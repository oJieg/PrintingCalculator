using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCupPrise : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.CutPrice == null)
            {
                result.ResultPaper.CutPrics = (int)(result.ResultPaper.PiecesPerSheet * 5); //брать из json 5-ку
                history.CutPrice = result.ResultPaper.CutPrics;
                return true;
            }
            else
            {
                result.ResultPaper.CutPrics =history.CutPrice;
                return true;
            }
        }
    }
}
