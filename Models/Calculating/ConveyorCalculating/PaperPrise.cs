using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPrise : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.PaperResult.Price = (int)(result.PaperResult.CostPrise + (result.PaperResult.CostPrise * (float)result.PaperResult.MarkupPaper / (float)100)) + result.PaperResult.CutPrics; //временное?
            return true;
        }
    }
}