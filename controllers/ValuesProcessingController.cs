using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    public class ValuesProcessingController : Controller
    {
        private ApplicationContext DB;
        public ValuesProcessingController(ApplicationContext context)
        {
            DB = context;
        }
        public IActionResult Index(int one)
        {
            TetsAddPaper();
            TestAddMarkup();
            TestAddConsumablePrice();
            List<PaperCatalog> catalog = DB.PaperCatalogs.Include(x=>x.Prices).ToList();
            ViewData["Massage"] = DB.Markups.First().Id.ToString();
            return View("PageOtvet", catalog);
        }

        private void TestAddMarkup()
        {
            Markup markup = new Markup
            {
                Markup0 = 300,
                Markup15 = 200,
                Markup30 = 100,
                Markup60 = 80,
                Markup120 = 50,
                Markup250 = 30,
                MarkupMuch = 25
            };
            DB.Markups.Add(markup);
            DB.SaveChanges();
        }

        private void TestAddConsumablePrice()
        {
            ConsumablePrice price = new ConsumablePrice
            {
                TonerPrice = 35000,
                DrumPrice1 = 20000,
                DrumPrice2 = 21000,
                DrumPrice3 = 24000,
                DrumPrice4 = 23000
            };
            DB.ConsumablePrices.Add(price);
            DB.SaveChanges();
        }

        private void TetsAddPaper()
        {
            SizePaper SRA3 = new SizePaper
            {
                NameSizePaper = "SRA3",
                SizePaperHeight = 320,
                SizePaperWidth = 450
            };
            PricePaper pricePaper = new PricePaper
            {
                Price = (float)17.7
            };
            PaperCatalog mondi350 = new PaperCatalog
            {
                Name = "mondi350",
                Prices = new List<PricePaper>() { pricePaper },
                Size = SRA3
            };
            DB.SizePapers.Add(SRA3);
            DB.PricePapers.Add(pricePaper);
            DB.PaperCatalogs.Add(mondi350);
            DB.SaveChanges();




            PricePaper pricePaper2 = new PricePaper
            {
                Price = (float)9.7
            };
            PaperCatalog mondi200 = new PaperCatalog
            {
                Name = "mondi 200",
                Prices = new List<PricePaper>() { pricePaper2 },
                Size = DB.SizePapers.Where(p => p.NameSizePaper == "SRA3").FirstOrDefault()
            };
            DB.PricePapers.Add(pricePaper2);
            DB.PaperCatalogs.Add(mondi200);
            DB.SaveChanges();
        }
    }
}
