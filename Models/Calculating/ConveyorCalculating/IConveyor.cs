using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public interface IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result);
    }
}