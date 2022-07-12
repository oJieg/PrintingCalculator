using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.Models;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using printing_calculator.Models.Settings;

namespace printing_calculator.controllers
{
    public class HistoryDetalController : Controller
    {
        private ApplicationContext _BD;
        private IOptions<Setting> _options;
        public HistoryDetalController(ApplicationContext context, IOptions<Setting> options)
        {
            _BD = context;
            _options = options;
        }

        public IActionResult Index(int id)
        {
            Result1 result = new();
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

