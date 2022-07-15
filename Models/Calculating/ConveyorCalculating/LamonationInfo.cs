using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationInfo : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            if (history.Input.Lamination != null)
            {
                result.ResultLamination = new ResultLamination();
                result.ResultLamination.Name = history.Input.Lamination.Name;
                return true;
            }
            return true;
        }
    }
}
