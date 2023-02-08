using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationPriсe : IConveyor
    {
        private readonly DataBase.setting.Setting _settings;

        public LamonationPriсe(DataBase.setting.Setting settings)
        {
            _settings = settings; 
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }

            try
            {
                if (history.Input.Lamination != null)
                {
                    int price = Convert.ToInt32((result.LaminationResult.CostPrice * ((result.LaminationResult.Markup + 100) / (float)100)) + _settings.Machines[0].AdjustmenPrice);
                    result.LaminationResult.Price = price;
                    result.Price += price;
                    return Task.FromResult((history, result, true));
                }

                result.LaminationResult.Price = 0;
                return Task.FromResult((history, result, true));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, false));
            }
        }
    }
}