using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosCreasing : IConveyor
    {
        private Pos _setting;
        public PosCreasing(Pos setting)
        {
            _setting = setting;
        }

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.ResultPos = new ResultPos();
            if (history.Input.CreasingAmount > 0)
            {
                result.ResultPos.CreasingAmount = history.Input.CreasingAmount;
                float CreasingPriceOneProduct = (int)((history.Input.CreasingAmount - 1) * _setting.CreasingAddHit) + _setting.CreasingOneProduct;
                result.ResultPos.CreasingPrice = (int)((CreasingPriceOneProduct * result.Amount) + (_setting.CreasingAdjustmen * result.Kinds));
            }
            else
            {
                result.ResultPos.CreasingAmount = 0;
                result.ResultPos.CreasingPrice = 0;
            }
            return true;
        }
    }
}
