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

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return (history, result, false);
            }

            result.PosResult.Rounding = history.Input.RoundingAmount;
            if (!result.PosResult.Rounding)
            {
                result.PosResult.ActualRoundingPrice = true;
                result.PosResult.RoundingPrice = 0;
                return (history, result, true);
            }
            int ActualPrice = (int)((result.Amount * _setting.RoundingOneProduct) + (result.Kinds * _setting.RoundingAdjustmen));
            int? Price = history.RoundingPrice;

            if (Price == null)
            {
                result.PosResult.RoundingPrice = ActualPrice;
                result.PosResult.ActualRoundingPrice = true;
                history.RoundingPrice = ActualPrice;
                return (history, result, true);
            }

            result.PosResult.RoundingPrice = (int)Price;
            if (ActualPrice == Price)
            {
                result.PosResult.ActualRoundingPrice = true;
                return (history, result, true);
            }

            result.PosResult.ActualRoundingPrice = false;
            return (history, result, true);
        }
    }
}