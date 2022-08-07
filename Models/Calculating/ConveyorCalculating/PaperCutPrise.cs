using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCutPrise : IConveyor
    {
        private readonly CutSetting _cutSetting;
        private const int AdjustmentCutOne = 30; //количество листов в одной привертке при резке

        public PaperCutPrise(CutSetting cutSetting)
        {
            _cutSetting = cutSetting;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                int CutOneAdjustmentPrice = (int)(result.PaperResult.PiecesPerSheet * _cutSetting.OneCutPrice) + _cutSetting.AdjustmentCutPrice;
                int CutPrice = CutOneAdjustmentPrice * (int)Math.Ceiling((double)result.PaperResult.Sheets / (double)AdjustmentCutOne);
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