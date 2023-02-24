﻿using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PosRounding : IConveyor
    {
        private readonly Setting _settings;

        public PosRounding(Setting settings)
        {
            _settings = settings;
        }

        public Task<(СalculationHistory, Result, bool)> TryConveyorStartAsync(СalculationHistory history, Result result, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult((history, result, false));
            }
            PosMachinesSetting? roundingSetting = _settings.PosMachines.Where(x => x.NameMachine == "rounding").FirstOrDefault();
            if (roundingSetting == null)
            {
                return Task.FromResult((history, result, false));
            }

            result.PosResult ??= new PosResult();

            result.PosResult.Rounding = history.Input.RoundingAmount;
            if (!result.PosResult.Rounding)
            {
                result.PosResult.ActualRoundingPrice = true;
                result.PosResult.RoundingPrice = 0;
                return Task.FromResult((history, result, true));
            }
            int actualPrice = (int)((result.Amount * roundingSetting.ConsumableOther) + (result.Kinds * roundingSetting.AdjustmenPrice));
            int? price = history.RoundingPrice;

            if (price == null)
            {
                result.PosResult.RoundingPrice = actualPrice;
                result.Price += actualPrice;
                result.PosResult.ActualRoundingPrice = true;
                history.RoundingPrice = actualPrice;
                return Task.FromResult((history, result, true));
            }

            result.PosResult.RoundingPrice = price.Value;
            result.Price += price.Value;

            result.PosResult.ActualRoundingPrice = (actualPrice == price);

            return Task.FromResult((history, result, true));
        }
    }
}