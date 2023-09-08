using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private readonly DataBase.setting.Setting _settings;
        private readonly ApplicationContext _applicationContext;

        public ConsumablePrice(DataBase.setting.Setting settings, ApplicationContext applicationContext)
        {
            _settings = settings;
            _applicationContext = applicationContext;
        }

        public async Task<(СalculationHistory, Result, StatusCalculation)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            try
            {
                //проверка на актуальность цен
                DataBase.ConsumablePrice? consumablePrice = await _applicationContext.ConsumablePrices
                    .AsNoTracking()
                    .OrderByDescending(сonsumablePrices => сonsumablePrices.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (consumablePrice == null)
                    return (history, result, new StatusCalculation() { 
                        Status = StatusAnswer.Other, 
                        ErrorMassage = "При обрашении к настройкам ConsumablePrices в базе, проихошла ошибка "
					});

                result.PaperResult.ActualConsumablePrice = consumablePrice.Id == history.ConsumablePrice.Id;

                float drumPrice = (float)(history.ConsumablePrice.DrumPrice1
                    + history.ConsumablePrice.DrumPrice2
                    + history.ConsumablePrice.DrumPrice3
                    + history.ConsumablePrice.DrumPrice4) / _settings.PrintingsMachines[0].MainConsumableForDrawing;
                float CMUKprice = (float)history.ConsumablePrice.TonerPrice / _settings.PrintingsMachines[0].ConsumableDye;
                float price = drumPrice + CMUKprice + _settings.PrintingsMachines[0].ConsumableOther;
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