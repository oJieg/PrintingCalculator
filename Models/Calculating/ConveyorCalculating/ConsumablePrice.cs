﻿using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class ConsumablePrice : IConveyor
    {
        private readonly Consumable _Consumable;
        private readonly ApplicationContext _DB;
        public ConsumablePrice(Consumable consumable, ApplicationContext DB)
        {
            _Consumable = consumable;
            _DB = DB;
        }
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                //проверка на актуальность цен
                int ActualConsumableId = _DB.ConsumablePrices.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                if(ActualConsumableId != history.ConsumablePrice.Id)
                {
                    result.PaperResult.ActualConsumablePrice = false;  
                }
                else
                {
                    result.PaperResult.ActualConsumablePrice = true;
                }


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
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}