using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationPriсe : IConveyor
    {
        private readonly Setting _settings;

        public LamonationPriсe(Setting settings)
        {
            _settings = settings; 
        }

        public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
				return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusType.Cancellation
				}));
			}

            try
            {
                if (history.Input.Lamination != null)
                {
                    int price = Convert.ToInt32((result.LaminationResult.CostPrice * ((result.LaminationResult.Markup + 100) / (float)100)) + _settings.Machines[0].AdjustmenPrice);
                    result.LaminationResult.Price = price;
                    result.Price += price;
                    return Task.FromResult((history, result, new StatusCalculation()
					{
						Status = StatusType.Ok
					}));
                }

                result.LaminationResult.Price = 0;
                return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusType.Ok
				}));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusType.Other,
                    ErrorMassage = "Расчетная цена вышла за возможные пределы Int"
				}));
            }
        }
    }
}