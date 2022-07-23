using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCupPrise : IConveyor
    {
        private readonly CutSetting _cutSetting;
        public PaperCupPrise(CutSetting cutSetting)
        {
            _cutSetting = cutSetting;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                if (history.CutPrice == null)
                {
                    result.PaperResult.CutPrics = (int)(result.PaperResult.PiecesPerSheet * _cutSetting.OneCutPrice) + _cutSetting.AdjustmentCutPrice; 
                    history.CutPrice = result.PaperResult.CutPrics;
                    return true;
                }
                else
                {
                    result.PaperResult.CutPrics = history.CutPrice;
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