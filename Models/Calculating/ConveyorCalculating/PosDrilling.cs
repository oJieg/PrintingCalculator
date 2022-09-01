using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosDrilling : IConveyor
    {
        private readonly Pos _setting;

        public PosDrilling(Pos setting)
        {
            _setting = setting;
        }

        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }
            if (result.PosResult == null)
                result.PosResult = new PosResult();

            result.PosResult.DrillingAmount = history.Input.DrillingAmount;
            if (history.Input.DrillingAmount == 0)
            {
                result.PosResult.ActualDrillingPrice = true;
                result.PosResult.DrillingPrice = 0;
                return Task.FromResult((history, result, true));
            }

            float drillingPriceOneProduct = (int)((history.Input.DrillingAmount - 1) * _setting.DrillingAddHit) + _setting.DrillingOneProduct;
            int actualPrice = (int)((drillingPriceOneProduct * result.Amount) + (_setting.DrillingAdjustmen * result.Kinds));
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
            if (actualPrice == price)
            {
                result.PosResult.ActualDrillingPrice = true;
                return Task.FromResult((history, result, true));
            }

            result.PosResult.ActualDrillingPrice = false;
            return Task.FromResult((history, result, true));
        }
    }
}