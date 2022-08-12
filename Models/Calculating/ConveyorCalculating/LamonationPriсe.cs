using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationPriсe : IConveyor
    {
        private readonly Settings.Lamination _setting;
        public LamonationPriсe(Settings.Lamination lamination)
        {
            _setting = lamination;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                if (history.Input.Lamination != null)
                {
                    result.LaminationResult.Price = (int)(result.LaminationResult.CostPrice * ((result.LaminationResult.Markup + 100) / (float)100)) + _setting.Adjustment;
                    return true;
                }

                result.LaminationResult.Price = 0;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}