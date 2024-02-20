using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPriсe : IConveyor
    {
        private readonly Setting _settings;

        public PaperPriсe(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
				return Task.FromResult((history, result, new StatusCalculation()
				{
					Status = StatusAnswer.Cancellation
				}));
			}

            try
            {
                int pricePaperWithMarkup = Convert.ToInt32(result.PaperResult.CostConsumablePrise +
                    (result.PaperResult.CostConsumablePrise * (float)result.PaperResult.MarkupPaper / (float)100));
                int pricePaper = pricePaperWithMarkup + result.PaperResult.CutPrics + _settings.PrintingsMachines[0].AdjustmenPrice;

                result.PaperResult.Price = pricePaper;
                result.Price += pricePaper;
                return Task.FromResult((history, result, new StatusCalculation()));
            }
            catch (OverflowException)
            {
                return Task.FromResult((history, result, new StatusCalculation() { 
                    Status = StatusAnswer.Other, 
                    ErrorMassage = "Стоимость бумаги вышла за возможные приделы int" }));
            }
        }
    }
}