using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private readonly Consumable _consumable;
        private readonly ApplicationContext _applicationContext;

        public ConsumablePrice(Consumable consumable, ApplicationContext applicationContext)
        {
            _consumable = consumable;
            _applicationContext = applicationContext;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            try
            {
                //проверка на актуальность цен
                DataBase.ConsumablePrice? consumablePrice = await _applicationContext.ConsumablePrices
                    .AsNoTracking()
                    .OrderByDescending(сonsumablePrices => сonsumablePrices.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                if (consumablePrice == null)
                    return (history, result, false);

                int actualConsumableId = consumablePrice.Id;

                result.PaperResult.ActualConsumablePrice = actualConsumableId == history.ConsumablePrice.Id;

                float drumPrice = (float)(history.ConsumablePrice.DrumPrice1
                    + history.ConsumablePrice.DrumPrice2
                    + history.ConsumablePrice.DrumPrice3
                    + history.ConsumablePrice.DrumPrice4) / (float)_consumable.Photoconductors;
                float CMUKprice = (float)history.ConsumablePrice.TonerPrice / (float)_consumable.CMYK;
                float price = drumPrice + CMUKprice + _consumable.Other;
                if (result.PaperResult.Duplex)
                {
                    price *= 2;
                }
                result.PaperResult.ConsumablePrice = price;
                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }
    }
}