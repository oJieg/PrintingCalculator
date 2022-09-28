using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosCreasing : IConveyor
    {
        private readonly Pos _setting;

        public PosCreasing(Pos setting)
        {
            _setting = setting;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
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

            float creasingPriceOneProduct = (int)((history.Input.CreasingAmount - 1) * _setting.CreasingAddHit) + _setting.CreasingOneProduct;
            int actualPrice = (int)((creasingPriceOneProduct * result.Amount) + (_setting.CreasingAdjustmen * result.Kinds));
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