using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperCutPriсe : IConveyor
    {
        private readonly Setting _settings;
        private  int? _countOfPapersInOneAdjustmentCut; //количество листов в одной привертке при резке

        public PaperCutPriсe(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }
            PosMachinesSetting? cuttingSetting = _settings.PosMachines.Where(x => x.NameMAchine == "cuting").FirstOrDefault();
            if (cuttingSetting == null) 
            {
                return Task.FromResult((history, result, false));
            }
            _countOfPapersInOneAdjustmentCut = cuttingSetting.CountOfPapersInOneAdjustmentCut;

            try
            {
                int cutOneAdjustmentPrice = Convert.ToInt32((result.PaperResult.PiecesPerSheet *
                    cuttingSetting.ConsumableOther) +
                    cuttingSetting.AdjustmenPrice);
                int cutPrice = cutOneAdjustmentPrice * (int)Math.Ceiling((double)result.PaperResult.Sheets / (double)_countOfPapersInOneAdjustmentCut);
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