using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCutPriсe : IConveyor
    {
        private readonly CutSetting _cutSetting;
        private const int CountOfPapersInOneAdjustmentCut = 30; //количество листов в одной привертке при резке

        public PaperCutPriсe(CutSetting cutSetting)
        {
            _cutSetting = cutSetting;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            try
            {
                int cutOneAdjustmentPrice = Convert.ToInt32((result.PaperResult.PiecesPerSheet * _cutSetting.OneCutPrice) + _cutSetting.AdjustmentCutPrice);
                int cutPrice = cutOneAdjustmentPrice * (int)Math.Ceiling((double)result.PaperResult.Sheets / (double)CountOfPapersInOneAdjustmentCut);
                if (history.CutPrice == null)
                {
                    result.PaperResult.CutPrics = cutPrice;
                    result.PaperResult.ActualCutPrice = true;
                    history.CutPrice = cutPrice;
                }
                else
                {
                    result.PaperResult.ActualCutPrice = (history.CutPrice == cutPrice);
                    result.PaperResult.CutPrics = (int)history.CutPrice;
                }
                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}