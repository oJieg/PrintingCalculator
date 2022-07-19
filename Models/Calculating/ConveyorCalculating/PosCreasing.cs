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

        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.PosResult = new PosResult();
            if (history.Input.CreasingAmount > 0)
            {
                result.PosResult.CreasingAmount = history.Input.CreasingAmount;
                float CreasingPriceOneProduct = (int)((history.Input.CreasingAmount - 1) * _setting.CreasingAddHit) + _setting.CreasingOneProduct;
                result.PosResult.CreasingPrice = (int)((CreasingPriceOneProduct * result.Amount) + (_setting.CreasingAdjustmen * result.Kinds));
            }
            else
            {
                result.PosResult.CreasingAmount = 0;
                result.PosResult.CreasingPrice = 0;
            }
            return true;
        }
    }
}