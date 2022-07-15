using printing_calculator.DataBase;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.Models.ConveyorCalculating
{
    public class AllPrice : IConveyor
    {
        public bool TryConveyorStart(ref History history, ref Result result)
        {
            result.Prise = (int)(result.ResultPaper.PrisePaper
                    + result.ResultLamination.Price
                    + result.ResultPos.CreasingPrice
                    + result.ResultPos.DrillingPrice
                    + result.ResultPos.RoundingPrice);

            if (history.Price == null)
            {
                result.TryTrice = true;
                history.Price = result.Prise;
                
            }

            if (history.Price != result.Prise)
			{
                result.TryTrice = false;
            }
            return true;
        }
    }
}
