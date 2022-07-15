using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPrise : IConveyor
    {
        private Settings.Lamination _lamination1;
        public LamonationCostPrise(Settings.Lamination lamination)
		{
            _lamination1 = lamination;
		}
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if(history.Input.Lamination != null)
            {
                result.ResultLamination.CostPrise =(int)((history.LaminationPrices.Price+_lamination1.Job) * result.ResultPaper.Sheets);
                return true;
            }
            return true;
        }
    }
}
