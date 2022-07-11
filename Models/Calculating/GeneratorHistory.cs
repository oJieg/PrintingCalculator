using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class GeneratorHistory
    {
        private ApplicationContext _DB;
        // private Input _input;
        private History _history;
        private HistoryInput _historyInput;

        public GeneratorHistory(ApplicationContext Db)
        {
            //  _input = inpyt;
            _DB = Db;
        }

        public bool Start(Result1 result)
        {
            HistoryInput(result);
            History(result);
            return true;
        }
        private void History(Result1 result)
        {
            _history = new();
            _history.Input = _historyInput;
            _history.PricePaper = _DB.PaperCatalogs
                .Include(x => x.Prices)
                .Where(p => p.Name == result.PaperName)
                .First().Prices.LastOrDefault();
            // .Input.Paper.Prices.Last();
            _history.ConsumablePrice = _DB.ConsumablePrices.FirstOrDefault();
            _history.MarkupPaper = (int)result.MarkupPaper;
        }

        private void HistoryInput(Result1 result)
        {
            _historyInput = new();
            _historyInput.Whidth = result.Whidth;
            _historyInput.Height = result.Height;
            _historyInput.Amount = result.Amount;
            _historyInput.Duplex = result.Duplex;
            _historyInput.Paper = _DB.PaperCatalogs
                .Include(x => x.Size)
                .Where(p => p.Name == result.PaperName)
                .FirstOrDefault();
            _historyInput.Kinds = result.Kinds;
        }
        public History GetHistory()
        {
            return _history;
        }

        public HistoryInput GetHistoryInput()
        {
            return _historyInput;
        }
    }
}
