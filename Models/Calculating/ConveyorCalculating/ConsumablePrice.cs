using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private readonly Consumable _Consumable;
        private readonly ApplicationContext _applicationContext;

        public ConsumablePrice(Consumable consumable, ApplicationContext applicationContext)
        {
            _Consumable = consumable;
            _applicationContext = applicationContext;
        }

        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result, CancellationToken cancellationToken)
        {
            try
            {
                //проверка на актуальность цен
                DataBase.ConsumablePrice consumablePrice = await _applicationContext.ConsumablePrices
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefaultAsync(cancellationToken);
                int ActualConsumableId = consumablePrice.Id;

                result.PaperResult.ActualConsumablePrice = ActualConsumableId == history.ConsumablePrice.Id;

                float drumPrice = (float)(history.ConsumablePrice.DrumPrice1
                    + history.ConsumablePrice.DrumPrice2
                    + history.ConsumablePrice.DrumPrice3
                    + history.ConsumablePrice.DrumPrice4) / (float)_Consumable.Photoconductors;
                float CMUKprice = (float)history.ConsumablePrice.TonerPrice / (float)_Consumable.CMYK;
                float price = drumPrice + CMUKprice + _Consumable.Other;
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