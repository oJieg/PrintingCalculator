using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.Models;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using printing_calculator.Models.markup;

namespace printing_calculator.controllers
{
    public class HistoryDetalController : Controller
    {
        private ApplicationContext _BD;
        private IOptions<Markup> _options;
        public HistoryDetalController(ApplicationContext context, IOptions<Markup> options)
        {
            _BD = context;
            _options = options;
        }

        public IActionResult Index(int id)
        {
            Result result = new();
            History histories = _BD.Historys
               .Include(x => x.Input)
               .Include(x => x.PricePaper.Catalog)
               .Where(x => x.Id == id)
               .FirstOrDefault();


            GeneratorResult generatorResult = new(_options);
            generatorResult.Start(histories);
            return View("CalculatorResult", generatorResult.GetResult());
        }

    }
}

