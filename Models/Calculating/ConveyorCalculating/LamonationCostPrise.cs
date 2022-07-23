using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPrise : IConveyor
    {
        private readonly Settings.Lamination _lamination1;
        public LamonationCostPrise(Settings.Lamination lamination)
        {
            _lamination1 = lamination;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {

            if (history.Input.Lamination != null)
            {
                if (_lamination1 == null)
                    return false;
                try
                {
                    result.LaminationResult.CostPrise = (int)((history.LaminationPrices.Price + _lamination1.Job) * result.PaperResult.Sheets);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
    }
}