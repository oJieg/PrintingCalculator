using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosDrilling : IConveyor
    {
        private Pos _setting;
        public PosDrilling(Pos setting)
        {
            _setting = setting;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.Input.DrillingAmount > 0)
            {
                result.ResultPos.DrillingAmount = history.Input.DrillingAmount;
                float DrillingPriceOneProduct = (int)((history.Input.DrillingAmount - 1) * _setting.DrillingAddHit) + _setting.DrillingOneProduct;
                result.ResultPos.DrillingPrice = (int)((DrillingPriceOneProduct * result.Amount) + (_setting.DrillingAdjustmen * result.Kinds));
            }
            else
            {
                result.ResultPos.DrillingAmount = 0;
                result.ResultPos.DrillingPrice = 0;
            }
            return true;
        }
    }
}
