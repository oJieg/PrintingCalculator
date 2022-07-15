using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationPrise : IConveyor
    {
        private Settings.Lamination _setting;
        public LamonationPrise(Settings.Lamination lamination)
        {
            _setting= lamination;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.Input.Lamination != null)
            {
                result.ResultLamination.Price = (result.ResultLamination.CostPrise * ((result.ResultLamination.Markup+100)/100)) + _setting.Adjustment;
                return true;
            }
            result.ResultLamination = new ResultLamination();
            result.ResultLamination.Price = 0;
            return true;
        }
    }
}
