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
            //мб валидацию добавить? 
            result.PosResult = new PosResult();

            result.PosResult.CreasingAmount = history.Input.CreasingAmount;
            if (history.Input.CreasingAmount == 0)
            {
                result.PosResult.CreasingPrice = 0;
                result.PosResult.ActualCreasingPrice = true;
                return true;
            }

            float CreasingPriceOneProduct = (int)((history.Input.CreasingAmount - 1) * _setting.CreasingAddHit) + _setting.CreasingOneProduct;
            int ActualPrice = (int)((CreasingPriceOneProduct * result.Amount) + (_setting.CreasingAdjustmen * result.Kinds));
            int? Price = history.CreasingPrice;

            if (Price == null)
            {
                history.CreasingPrice = ActualPrice;
                result.PosResult.CreasingPrice = ActualPrice;
                result.PosResult.ActualCreasingPrice = true;
                return true;
            }

            result.PosResult.CreasingPrice =(int)Price;
            if(Price == ActualPrice)
            {
                result.PosResult.ActualCreasingPrice = true;
                return true;
            }

            result.PosResult.ActualCreasingPrice=false;
            return true;
        }
    }
}