using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationInfo : IConveyor
    {
        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result)
        {
            result.LaminationResult = new();
            if (history.Input.Lamination != null)
            {
                try
                {
                    result.LaminationResult.Name = history.Input.Lamination.Name;
                    return (history, result, true);
                }
                catch
                {
                    return (history, result, false);
                }
            }
            return (history, result, true);
        }
    }
}