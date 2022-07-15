using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosRounding : IConveyor
    {
        private Pos _setting;
        public PosRounding(Pos setting)
        {
            _setting = setting;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.ResultPos.Rounding = history.Input.RoundingAmount;

            if (history.Input.RoundingAmount)
            {
                result.ResultPos.RoundingPrice = (int)((result.Amount * _setting.RoundingOneProduct) + (result.Kinds * _setting.RoundingAdjustmen));
            }

            return true;
        }
    }
}