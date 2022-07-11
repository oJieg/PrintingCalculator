using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperPrise : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.ResultPaper.PrisePaper = (int)(result.ResultPaper.CostPrise + (result.ResultPaper.CostPrise * (float)result.ResultPaper.MarkupPaper / 100))+result.ResultPaper.CutPrics; //временное
            return true;
        }
    }
}
