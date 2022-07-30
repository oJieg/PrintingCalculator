using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class Info : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            try
            {
                result.HistoryInputId = history.Id;
                result.Amount = history.Input.Amount;
                result.Kinds = history.Input.Kinds;
                result.Height = history.Input.Height;
                result.Whidth = history.Input.Whidth;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}