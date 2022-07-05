using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class GeneratorHistory : IStartGeneration
    {
        private ApplicationContext _DB;
        private Input _input;
        private History _history;
        private HistoryInput _historyInput;

        public GeneratorHistory(Input inpyt, ApplicationContext Db)
        {
            _input = inpyt;
            _DB = Db;
        }

        public bool Start()
        {
            HistoryInput();
            History();
            return true;
        }
        private void History()
        {
            _history = new();
            _history.Input = _historyInput;
            _history.PricePaper = _DB.PaperCatalogs.Include(x => x.Prices).Where(p => p.Name == _input.Paper).First().Prices.LastOrDefault();
            // .Input.Paper.Prices.Last();
            _history.ConsumablePrice = _DB.ConsumablePrices.FirstOrDefault();
            _history.Markup = _DB.Markups.First();
        }

        private void HistoryInput()
        {
            _historyInput = new();
            _historyInput.Whidth = _input.Whidth;
            _historyInput.Height = _input.Height;
            _historyInput.Amount = _input.Amount;
            _historyInput.Duplex = _input.Duplex;
            _historyInput.Paper = _DB.PaperCatalogs.Include(x=> x.Size).Where(p => p.Name == _input.Paper).FirstOrDefault();
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
