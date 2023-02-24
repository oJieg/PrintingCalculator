using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosDrilling : IConveyor
    {
        private readonly Setting _settings;

        public PosDrilling(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            PosMachinesSetting? drillingSetting = _settings.PosMachines.Where(x => x.NameMachine == "drilling").FirstOrDefault();
            if (drillingSetting == null)
            {
                return Task.FromResult((history, result, false));
            }

            result.PosResult ??= new();

            result.PosResult.DrillingAmount = history.Input.DrillingAmount;
            if (history.Input.DrillingAmount == 0)
            {
                result.PosResult.ActualDrillingPrice = true;
                result.PosResult.DrillingPrice = 0;
                return Task.FromResult((history, result, true));
            }


            float drillingPriceOneProduct = (int)((history.Input.DrillingAmount - 1) * drillingSetting.AddMoreHit) + drillingSetting.ConsumableOther;
            int actualPrice = (int)((drillingPriceOneProduct * result.Amount) + (drillingSetting.AdjustmenPrice * result.Kinds));
            int? price = history.DrillingPrice;

            if (price == null)
            {
                history.DrillingPrice = actualPrice;
                result.PosResult.DrillingPrice = actualPrice;
                result.Price += actualPrice;
                result.PosResult.ActualDrillingPrice = true;
                return Task.FromResult((history, result, true));
            }

            result.PosResult.DrillingPrice = price.Value;
            result.Price += price.Value;

            result.PosResult.ActualDrillingPrice = actualPrice == price;

            return Task.FromResult((history, result, true));
        }
    }
}