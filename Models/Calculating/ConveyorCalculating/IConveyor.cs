using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public interface IConveyor
    {
        public Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result);
    }
}