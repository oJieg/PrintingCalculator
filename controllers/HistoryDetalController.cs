using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.Models;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace printing_calculator.controllers
{
    public class HistoryDetalController : Controller
    {
        private ApplicationContext _BD;
        public HistoryDetalController(ApplicationContext context)
        {
            _BD = context;
        }

        public IActionResult Index(int id)
        {
            Result result = new();
            History histories = _BD.Historys
               .Include(x => x.Input)
               .Include(x => x.PricePaper.Catalog)
               .Where(x => x.Id == id)
               .FirstOrDefault();


            GeneratorResult generatorResult = new();
            generatorResult.Start(histories);
            return View("CalculatorResult", generatorResult.GetResult());
        }

    }
}

