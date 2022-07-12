using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.Models;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private Consumable _Consumable;
        public ConsumablePrice(Consumable consumable)
        {
            _Consumable = consumable;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            float drumPrice = (float)(history.ConsumablePrice.DrumPrice1
                + history.ConsumablePrice.DrumPrice2
                + history.ConsumablePrice.DrumPrice3
                + history.ConsumablePrice.DrumPrice4)/(float)_Consumable.Photoconductors;
            float CMUKprice =(float) history.ConsumablePrice.TonerPrice / (float)_Consumable.CMYK;
            float price = drumPrice + CMUKprice + _Consumable.Other;
            if (result.ResultPaper.Duplex)
            {
                price *= 2;
            }
            result.ResultPaper.ConsumablePrice = price;
            return true;
        }
    }
}
