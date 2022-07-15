using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using printing_calculator.Models;
using printing_calculator.Models.ConveyorCalculating;
using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;


namespace printing_calculator.Models.Calculating
{
    public class ConveyorCalculator
    {
        private Setting _settings;
        public ConveyorCalculator(Setting options)
        {
            _settings = options;
        }

        public bool TryStartCalculation(ref History history, out Result result)
        {
            List<IConveyor> conveyors = new()
            {
                new Info(),
                new PaperInfo(),
                new PaperSplitting(_settings.SettingPrinter),
                new ConveyorCalculating.ConsumablePrice(_settings.Consumable),
                new PaperCostPrice(_settings),
                new PaperMarkup(_settings.MarkupPaper),
                new PaperCupPrise(_settings.CutSetting),
                new PaperPrise(),
                new LamonationInfo(),
                new LamonationMarkup(_settings.Lamination),
                new LamonationCostPrise(_settings.Lamination),
                new ConveyorCalculating.LamonationPrise(_settings.Lamination),
                new PosCreasing(_settings.Pos),
                new PosDrilling(_settings.Pos),
                new PosRounding(_settings.Pos),
                new AllPrice(),

                
            };
            result = new();
            result.ResultPaper = new ResultPaper();


            foreach (var conveyor in conveyors)
            {
                conveyor.TryConveyorStart(ref history,ref result);
            }

            return true;
        }
    }
}
