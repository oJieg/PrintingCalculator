using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

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
                //----------------------
                await addDefoltSettingMashine();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка записи ConsumablePrice");
            }
        }

        private async Task addDefoltSettingMashine()
        {
            Markups printMarkups0 = new()
            {
                Page = 0,
                MarkupForThisPage = 350
            };
            Markups printMarkups10 = new()
            {
                Page = 10,
                MarkupForThisPage = 300
            };
            Markups printMarkups50 = new()
            {
                Page = 50,
                MarkupForThisPage = 200
            };
            Markups printMarkups100 = new()
            {
                Page = 100,
                MarkupForThisPage = 175
            };
            Markups printMarkups250 = new()
            {
                Page = 250,
                MarkupForThisPage = 90
            };

            Markups laminationMarkups0 = new()
            {
                Page = 0,
                MarkupForThisPage = 120
            };
            Markups laminationMarkups30 = new()
            {
                Page = 30,
                MarkupForThisPage = 100
            };
            Markups laminationMarkups150 = new()
            {
                Page = 150,
                MarkupForThisPage = 10
            };

            Markups markupZiro1 = new()
            {
                Page = 0,
                MarkupForThisPage = 0
            };
            Markups markupZiro2 = new()
            {
                Page = 0,
                MarkupForThisPage = 0
            };
            Markups markupZiro3 = new()
            {
                Page = 0,
                MarkupForThisPage = 0
            };
            Markups markupZiro4 = new()
            {
                Page = 0,
                MarkupForThisPage = 0
            };

            List<Markups> printMarkups = new() { printMarkups0,printMarkups10,printMarkups50,printMarkups100,printMarkups250 };
            List<Markups> laminationMarkup = new() { laminationMarkups0,laminationMarkups30,laminationMarkups150 };

            PrintingMachineSetting printingMachine = new()
            {
                NameMAchine = "180",
                Markup = new(printMarkups),
                ConsumableOther = 3,
                AdjustmenPrice = 0,

                WhiteFieldHeight = 6,
                WhiteFieldWidth = 5,

                FieldForLabels = 4,
                Bleed = 3,

                MaximumSizeLength = 2000,
                MaximumSizeWidth = 320,

                ConsumableDye = 9100,
                MainConsumableForDrawing = 42900
            };
            MachineSetting Laminator = new()
            {
                NameMAchine = "laminator",
                Markup = new(laminationMarkup),
                ConsumableOther = 30,
                AdjustmenPrice = 50
            };
            PosMachinesSetting cut = new()
            {
                NameMAchine = "cuting",
                Markup = new(1) { markupZiro1},
                ConsumableOther = 0,
                AdjustmenPrice = 0,
                CountOfPapersInOneAdjustmentCut = 100,
                AddMoreHit = 0
            };
            PosMachinesSetting Creasing = new()
            {
                NameMAchine = "creasing",
                Markup = new(1) { markupZiro2},
                ConsumableOther = 1,
                AdjustmenPrice = 150,
                CountOfPapersInOneAdjustmentCut = 1,
                AddMoreHit = 1
            };
            PosMachinesSetting Drilling = new()
            {
                NameMAchine = "drilling",
                Markup = new(1) { markupZiro3},
                ConsumableOther = 0.6f,
                AdjustmenPrice = 150,
                CountOfPapersInOneAdjustmentCut = 1,
                AddMoreHit = 0.6f
            };
            PosMachinesSetting Rounding = new()
            {
                NameMAchine = "rounding",
                Markup = new(1) { markupZiro4},
                ConsumableOther = 0.5f,
                AdjustmenPrice = 150,
                CountOfPapersInOneAdjustmentCut = 1,
                AddMoreHit = 0
            };
            Setting setting = new()
            {
                PrintingsMachines = new() { printingMachine },
                Machines = new() { Laminator },
                PosMachines = new() { cut, Creasing, Drilling, Rounding }
            };

            _applicationContext.Markups.AddRange(printMarkups0, printMarkups10, printMarkups50, printMarkups100, printMarkups250);
            _applicationContext.Markups.AddRange(laminationMarkups0, laminationMarkups30, laminationMarkups150);
            _applicationContext.Markups.AddRange(markupZiro1,markupZiro2,markupZiro3,markupZiro4);

            _applicationContext.MachineSettings.Add(Laminator);
            _applicationContext.PrintingMachinesSettings.Add(printingMachine);
            _applicationContext.PosMachinesSettings.Add(cut);
            _applicationContext.PosMachinesSettings.Add(Creasing);
            _applicationContext.PosMachinesSettings.Add(Drilling);
            _applicationContext.PosMachinesSettings.Add(Rounding);

            _applicationContext.Settings.Add(setting);
            await _applicationContext.SaveChangesAsync();
        }

    }
}