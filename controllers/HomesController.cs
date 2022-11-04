using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    public class HomesController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<HomesController> _logger;

        public HomesController(ILogger<HomesController> logger, ApplicationContext applicationContex)
        {
            _logger = logger;
            _applicationContext = applicationContex;
        }

        public async Task<IActionResult> Index()
        {
            if (_applicationContext.ConsumablePrices.Count() == 0)
            {
                await addDefaultValues();
                _logger.LogTrace("добавлены дефолтные значения");
            }
            return View("Page");
        }

        public IActionResult Changelog()
        {
            return View("Chengelog");
        }

        private async Task addDefaultValues()
        {
            try
            {
                ConsumablePrice price = new()
                {
                    TonerPrice = 45100,
                    DrumPrice1 = 28100,
                    DrumPrice2 = 28100,
                    DrumPrice3 = 28100,
                    DrumPrice4 = 28100
                };


                SizePaper SRA3 = new()
                {
                    Name = "SRA3",
                    Height = 320,
                    Width = 450
                };
                SizePaper a4 = new()
                {
                    Name = "A4",
                    Height = 210,
                    Width = 297
                };
                _applicationContext.ConsumablePrices.Add(price);
                _applicationContext.SizePapers.Add(SRA3);
                _applicationContext.SizePapers.Add(a4);
                await _applicationContext.SaveChangesAsync();
                PaperCatalog zeroPaper = new()
                {
                    Name = "Нулевая бумага",
                    Prices = 0,
                    Size = _applicationContext.SizePapers.Where(x => x.Name == "SRA3").First(),
                    Status = 1

                };

                _applicationContext.PaperCatalogs.Add(zeroPaper);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка записи ConsumablePrice");
            }
        }

    }
}