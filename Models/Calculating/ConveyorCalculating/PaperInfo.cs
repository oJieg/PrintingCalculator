using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class PaperInfo : IConveyor
    {
        public async Task<(History, Result, bool)> TryConveyorStartAsync(History history, Result result)
        {
            try
            {
                result.PaperResult.NamePaper = history.Input.Paper.Name;
                result.PaperResult.Duplex = history.Input.Duplex;

                return (history, result, true);
            }
            catch
            {
                return (history, result, false);
            }
        }
    }
}