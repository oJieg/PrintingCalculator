using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPriсe : IConveyor
    {
        private readonly Setting _settings;
        private readonly ApplicationContext _applicationContext;

        public LamonationCostPriсe(Setting settings, ApplicationContext applicationContext)
        {
            _settings= settings;
            _applicationContext = applicationContext;
        }

        public async Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (history.Input.Lamination == null || history.LaminationPrices == null)
            {
                result.LaminationResult.ActualCostPrics = true;
                return (history, result, new StatusCalculation());
            }

            try
            {
                int сostPrice = Convert.ToInt32((history.LaminationPrices + _settings.Machines[0].ConsumableOther) * result.PaperResult.Sheets);
                result.LaminationResult.CostPrice = сostPrice;

                result.LaminationResult.ActualCostPrics =
                    await ActualCostPrice(history.LaminationPrices, history.Input.Lamination.Name, cancellationToken);
                return (history, result, new StatusCalculation());
            }
            catch (OverflowException)
            {
                return (history, result, new StatusCalculation() { 
                    Status = StatusAnswer.Other, 
                    ErrorMassage = "Стоимость расходных материалов ламинации вышла за возможные приделы int" });
            }
        }

        private async Task<bool> ActualCostPrice(float? laminationPrices, string laminationName, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.Laminations
                    .AsNoTracking()
                    .Where(laminations => laminations.Name == laminationName)
                    .Select(Laminations => Laminations.Price == laminationPrices)
                    .FirstAsync(cancellationToken);
            }
            catch
            {
                return false;
            }
        }
    }
}