using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCupPrise : IConveyor
    {
        private CutSetting _cutSetting;
        public PaperCupPrise(CutSetting cutSetting)
        {
            _cutSetting = cutSetting;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.CutPrice == null)
            {
                result.ResultPaper.CutPrics = (int)(result.ResultPaper.PiecesPerSheet * _cutSetting.OneCutPrice)+ _cutSetting.AdjustmentCutPrice; //брать из json 5-ку
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
