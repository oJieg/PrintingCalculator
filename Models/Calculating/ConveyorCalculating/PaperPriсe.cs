using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPriсe : IConveyor
    {
        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result)
        {
            result.PaperResult.Price = (int)(result.PaperResult.CostPrise + (result.PaperResult.CostPrise * (float)result.PaperResult.MarkupPaper / (float)100)) + result.PaperResult.CutPrics; //временное?
            return (history, result, true);
        }
    }
}