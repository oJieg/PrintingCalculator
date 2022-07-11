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
        private IOptions<Settings> _settings;
        public ConveyorCalculator(IOptions<Settings> options)
        {
            _settings = options;
        }

        public bool TryStartCalculation(ref History history, out Result result)
        {
            List<IConveyor> conveyors = new()
            { 
                new Info(),
                new PaperInfo(),
                new PaperSplitting(_settings), //json
                new PaperCostPrice(_settings.Value),
                new PaperMarkup(_settings.Value.MarkupPaper),
                new PaperCupPrise(), //json
                new PaperPrise()
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
