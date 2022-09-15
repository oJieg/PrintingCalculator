using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosRounding : IConveyor
    {
        private readonly Pos _setting;

        public PosRounding(Pos setting)
        {
            _setting = setting;
        }

        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromResult((history, result, false));

            result.PosResult ??= new PosResult();

            result.PosResult.Rounding = history.Input.RoundingAmount;
            if (!result.PosResult.Rounding)
            {
                result.PosResult.ActualRoundingPrice = true;
                result.PosResult.RoundingPrice = 0;
                return Task.FromResult((history, result, true));
            }
            int actualPrice = (int)((result.Amount * _setting.RoundingOneProduct) + (result.Kinds * _setting.RoundingAdjustmen));
            int? price = history.RoundingPrice;

            if (price == null)
            {
                result.PosResult.RoundingPrice = actualPrice;
                result.Price += actualPrice;
                result.PosResult.ActualRoundingPrice = true;
                history.RoundingPrice = actualPrice;
                return Task.FromResult((history, result, true));
            }

            result.PosResult.RoundingPrice = price.Value;
            result.Price += price.Value;

            result.PosResult.ActualRoundingPrice = (actualPrice == price);

            return Task.FromResult((history, result, true));
        }
    }
}