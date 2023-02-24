using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosCreasing : IConveyor
    {
        private readonly Setting _settings;

        public PosCreasing(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            PosMachinesSetting? CreasingSetting = _settings.PosMachines.Where(x => x.NameMachine == "creasing").FirstOrDefault();
            if (CreasingSetting == null)
            {
                return Task.FromResult((history, result, false));
            }

            result.PosResult.CreasingAmount = history.Input.CreasingAmount;

            if (history.Input.CreasingAmount == 0)
            {
                result.PosResult.CreasingPrice = 0;
                result.PosResult.ActualCreasingPrice = true;
                return Task.FromResult((history, result, true));
            }
            

            float creasingPriceOneProduct = (int)((history.Input.CreasingAmount - 1) * CreasingSetting.AddMoreHit) + CreasingSetting.ConsumableOther;
            int actualPrice = (int)((creasingPriceOneProduct * result.Amount) + (CreasingSetting.AdjustmenPrice * result.Kinds));
            int? price = history.CreasingPrice;

            if (price == null)
            {
                history.CreasingPrice = actualPrice;
                result.PosResult.CreasingPrice = actualPrice;
                result.PosResult.ActualCreasingPrice = true;
                result.Price += actualPrice;
                return Task.FromResult((history, result, true));
            }

            result.PosResult.CreasingPrice = price.Value;
            result.Price += price.Value;

            result.PosResult.ActualCreasingPrice = price == actualPrice;
            return Task.FromResult((history, result, true));
        }
    }
}