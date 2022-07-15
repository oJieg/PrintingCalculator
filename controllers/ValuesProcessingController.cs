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
            TestAddConsumablePrice();
            TetsAddPaper("CC - 350", (float)17.2);
            TetsAddPaper("CC - 400", (float)23.04);
            TetsAddPaper("DNS - 200", (float)10.58);
            TetsAddPaper("DNS - 160", (float)8.0);
            TetsAddPaper("DNS - 90", (float)4.32);
            TetsAddPaper("DP - 200", (float)8.6);
            TetsAddPaper("DP - 170", (float)7.6);
            TetsAddPaper("DP - 130", (float)2.56);


            TestAddLamonation();
            List<PaperCatalog> catalog = DB.PaperCatalogs.Include(x => x.Prices).ToList();
            // ViewData["Massage"] = DB.Markups.First().Id.ToString();
            return View("PageOtvet", catalog);
        }

        private void TestAddConsumablePrice()
        {
            ConsumablePrice price = new ConsumablePrice
            {
                TonerPrice = 21300,
                DrumPrice1 = 28700,
                DrumPrice2 = 28700,
                DrumPrice3 = 28700,
                DrumPrice4 = 28700
            };
            DB.ConsumablePrices.Add(price);

            SizePaper SRA3 = new SizePaper
            {
                NameSizePaper = "SRA3",
                SizePaperHeight = 320,
                SizePaperWidth = 450
            };
            DB.SizePapers.Add(SRA3);
            DB.SaveChanges();
        }
        private void TestAddLamonation()
        {
            LaminationPrice price = new LaminationPrice
            {
                Price = (float)5.96
            };
            Lamination lamination = new Lamination
            {
                Name = "Матовая 35мкр",
                Price = new List<LaminationPrice>() { price }
            };

            LaminationPrice price2 = new LaminationPrice
            {
                Price = (float)16.04
            };
            Lamination lamination2 = new Lamination
            {
                Name = "софт тач 35мкр",
                Price = new List<LaminationPrice>() { price2 }
            };

            DB.LaminationPrices.Add(price);
            DB.laminations.Add(lamination);
            DB.LaminationPrices.Add(price2);
            DB.laminations.Add(lamination2);
            DB.SaveChanges();
        }

        private void TetsAddPaper(string namePaper, float Price)
        {
            PricePaper pricePaper = new PricePaper
            {
                Price = Price
            };
            PaperCatalog mondi350 = new PaperCatalog
            {
                Name = namePaper,
                Prices = new List<PricePaper>() { pricePaper },
                Size = DB.SizePapers.Where(x => x.NameSizePaper == "SRA3").First()
            };
            DB.PricePapers.Add(pricePaper);
            DB.PaperCatalogs.Add(mondi350);
            DB.SaveChanges();
        }
    }
}
