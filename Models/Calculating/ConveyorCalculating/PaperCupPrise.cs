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
                int CutPrice = (int)(result.PaperResult.PiecesPerSheet * _cutSetting.OneCutPrice) + _cutSetting.AdjustmentCutPrice;
                if (history.CutPrice == null)
                {
                    result.PaperResult.CutPrics = CutPrice;
                    result.PaperResult.ActualCutPrics = true;
                    history.CutPrice = CutPrice;
                    return true;
                }
                else
                {
                    result.PaperResult.ActualCutPrics = ActualCutPrice(history.CutPrice, CutPrice);
                    result.PaperResult.CutPrics = history.CutPrice;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool ActualCutPrice(int? historyPrice, int cutPrice)
        {
            if(historyPrice == cutPrice)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}