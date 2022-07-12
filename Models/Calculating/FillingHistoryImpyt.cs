using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models.Calculating
{
    public class FillingHistoryImpyt
    {
        private ApplicationContext _DB;
        public FillingHistoryImpyt(ApplicationContext DB)
        {
            _DB = DB;
        }

        public History GeneratorHistory(Input input)
        {
            History history = new History();
            history.Input = new HistoryInput();
            history.Input.Height = input.Height;
            history.Input.Whidth = input.Whidth;
            history.Input.Amount = input.Amount;
            history.Input.Kinds = input.Kinds;
            history.Input.Duplex = input.Duplex;
            history.Input.CreasingAmount = input.Creasing;
            history.Input.DrillingAmount = input.Drilling;
            history.Input.RoundingAmount = input.Rounding;

            history.Input.Paper = _DB.PaperCatalogs
                .Include(x => x.Prices)
                .Include(x => x.Size)
                .Where(p => p.Name == input.Paper)
                .First();

            if(input.LaminationName != null)
            {
                history.Input.Lamination = _DB.laminations
                    .Include(x => x.Price)
                    .Where(x => x.Name == input.LaminationName)
                    .First();
            }
            history.ConsumablePrice = _DB.ConsumablePrices
                .First();
            history.Input.CreasingAmount = input.Creasing;
            history.Input.DrillingAmount = input.Drilling;
            history.Input.RoundingAmount= input.Rounding;
            history.PricePaper = history.Input.Paper.Prices.FirstOrDefault(); //тут как то странное... явно может взять не последнию цену
            history.ConsumablePrice = _DB.ConsumablePrices.FirstOrDefault();

            return history;
        }
    }
}
