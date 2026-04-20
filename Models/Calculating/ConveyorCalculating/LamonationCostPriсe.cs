using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class LamonationCostPriсe : IConveyor
    {
        private readonly ISettingCalculation _settings;

        public LamonationCostPriсe(ISettingCalculation settings)
        {
            _settings= settings;
        }

        public async Task<(СalculationHistory, CalculationResult, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, CalculationResult result, CancellationToken cancellationToken)
        {
            if (history.Input.Lamination == null || history.LaminationPrices == null)
            {
                return (history, result, new StatusCalculation());
            }

            try
            {
                int сostPrice = Convert.ToInt32((history.LaminationPrices + _settings.PrintingsMachine.ConsumableOther) * result.PaperResult.Sheets);
                result.LaminationResult.CostPrice = сostPrice;

                return (history, result, new StatusCalculation());
            }
            catch (OverflowException)
            {
                return (history, result, new StatusCalculation() { 
                    Status = StatusAnswer.Other, 
                    ErrorMassage = "Стоимость расходных материалов ламинации вышла за возможные приделы int" });
            }
        }
    }
}