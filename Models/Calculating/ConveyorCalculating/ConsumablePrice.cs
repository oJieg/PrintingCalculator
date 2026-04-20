using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private readonly ISettingCalculation _settings;

        public ConsumablePrice(ISettingCalculation settings)
        {
            _settings = settings;
        }

        public async Task<(СalculationHistory, CalculationResult, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, CalculationResult result, CancellationToken cancellationToken)
        {
            try
            {

                float drumPrice = (float)(history.ConsumablePrice.DrumPrice1
                    + history.ConsumablePrice.DrumPrice2
                    + history.ConsumablePrice.DrumPrice3
                    + history.ConsumablePrice.DrumPrice4) / _settings.PrintingsMachine.MainConsumableForDrawing;
                float CMUKprice = (float)history.ConsumablePrice.TonerPrice / _settings.PrintingsMachine.ConsumableDye;
                float price = drumPrice + CMUKprice + _settings.PrintingsMachine.ConsumableOther;
                if (result.PaperResult.Duplex)
                {
                    price *= 2;
                }
                result.PaperResult.ConsumablePrinterPrice = price;
                return (history, result, new StatusCalculation());
            }
            catch
            {
                return (history, result, new StatusCalculation() { 
                    Status = StatusAnswer.Other,
                    ErrorMassage = "Произошла ошибка при вычислении ConsumablePrice"
				});
            }
        }
    }
}