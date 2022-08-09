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

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.PosResult.Rounding = history.Input.RoundingAmount;
            if (!result.PosResult.Rounding)
            {
                result.PosResult.ActualRoundingPrice = true;
                result.PosResult.RoundingPrice = 0;
                return true;
            }
            int ActualPrice = (int)((result.Amount * _setting.RoundingOneProduct) + (result.Kinds * _setting.RoundingAdjustmen));
            int? Price = history.RoundingPrice;

            if (Price == null)
            {
                result.PosResult.RoundingPrice = ActualPrice;
                result.PosResult.ActualRoundingPrice = true;
                history.RoundingPrice = ActualPrice;
                return true;
            }

            if (ActualPrice == Price)
            {
                result.PosResult.RoundingPrice = Price.Value;
                result.PosResult.ActualRoundingPrice = true;
                return true;
            }

            result.PosResult.RoundingPrice = (int)Price;
            result.PosResult.ActualRoundingPrice = false;
            return true;
        }
    }
}