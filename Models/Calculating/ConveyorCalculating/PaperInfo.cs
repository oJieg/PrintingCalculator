using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperInfo :IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.ResultPaper.NamePaper = history.Input.Paper.Name;
            result.ResultPaper.Duplex = history.Input.Duplex;

            return true;
        }
    }
}
