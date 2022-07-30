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
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.PosResult.DrillingAmount = history.Input.DrillingAmount;
            if (history.Input.DrillingAmount == 0)
            {
                result.PosResult.ActualDrillingPrice = true;
                result.PosResult.DrillingPrice = 0;
                return true;
            }

            float DrillingPriceOneProduct = (int)((history.Input.DrillingAmount - 1) * _setting.DrillingAddHit) + _setting.DrillingOneProduct;
            int actualPrice = (int)((DrillingPriceOneProduct * result.Amount) + (_setting.DrillingAdjustmen * result.Kinds));
            int? Price = history.DrillingPrice;

            if (Price == null)
            {
                history.DrillingPrice = actualPrice;
                result.PosResult.DrillingPrice = actualPrice;
                result.PosResult.ActualDrillingPrice = true;
                return true;
            }

            result.PosResult.DrillingPrice = (int)Price;
            if (actualPrice == Price)
            {
                result.PosResult.ActualDrillingPrice = true;
                return true;
            }

            result.PosResult.ActualDrillingPrice = true;
            return true;
        }
    }
}