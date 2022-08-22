﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    //DOTO: Веременная заглушка. Удалить в след патче, после добавления функционала изменения каталога бумаги и цен расходников.
    public class ValuesProcessingController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<HomesController> _logger;
        public ValuesProcessingController(ApplicationContext applicationContext, ILogger<HomesController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Run AddTest data");
           // TestAddConsumablePrice();
            //TetsAddPaper("CC - 350", (float)17.2);
            //TetsAddPaper("CC - 400", (float)23.04);
            //TetsAddPaper("DNS - 200", (float)10.58);
            //TetsAddPaper("DNS - 160", (float)8.0);
            //TetsAddPaper("DNS - 90", (float)4.32);
            //TetsAddPaper("DP - 200", (float)8.6);
            //TetsAddPaper("DP - 170", (float)7.6);
            //TetsAddPaper("DP - 130", (float)2.56);
            //TestAddLamonation("матовая 1+1", (float)5.96);
            //TestAddLamonation("софт тач 1+1", (float)16.04);

            List<PaperCatalog> catalog = _applicationContext.PaperCatalogs.Include(paperCatalogs => paperCatalogs.Prices).ToList();
            // ViewData["Massage"] = DB.Markups.First().Id.ToString();
            return View("PageOtvet", catalog);
        }

        private async void TestAddConsumablePriceAsync()
        {
            try
            {
                ConsumablePrice price = new()
                {
                    //TonerPrice = 45000,
                    //DrumPrice1 = 28700,
                    //DrumPrice2 = 28700,
                    //DrumPrice3 = 28700,
                    //DrumPrice4 = 28700
                    TonerPrice = 45100,
                    DrumPrice1 = 28100,
                    DrumPrice2 = 28100,
                    DrumPrice3 = 28100,
                    DrumPrice4 = 28100
                };
                _applicationContext.ConsumablePrices.Add(price);

                SizePaper SRA3 = new()
                {
                    NameSizePaper = "SRA3",
                    SizePaperHeight = 320,
                    SizePaperWidth = 450
                };
                _applicationContext.SizePapers.Add(SRA3);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка записи ConsumablePrice");
            }
            finally
            {
                _logger.LogInformation("add AddConsumablePrice");
            }
        }
        private async void TestAddLamonationAsync(string nameLamonation, float price)
        {
            try
            {
                LaminationPrice Prices = new()
                {
                    Price = price
                };
                Lamination lamination = new()
                {
                    Name = nameLamonation,
                    Price = new List<LaminationPrice>() { Prices }
                };

                _applicationContext.LaminationPrices.Add(Prices);
                _applicationContext.Laminations.Add(lamination);

                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка записи AddLamonation");
            }
            finally
            {
                _logger.LogInformation("add Lamination {nameLamonation}: {price}", nameLamonation, price);
            }
        }

        private async void TetsAddPaperAsync(string namePaper, float price)
        {
            try
            {
                PricePaper pricePaper = new()
                {
                    Price = price
                };
                PaperCatalog mondi350 = new()
                {
                    Name = namePaper,
                    Prices = new List<PricePaper>() { pricePaper },
                    Size = _applicationContext.SizePapers.Where(x => x.NameSizePaper == "SRA3").First()
                };
                _applicationContext.PricePapers.Add(pricePaper);
                _applicationContext.PaperCatalogs.Add(mondi350);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка записи AddPaper");
            }
            finally
            {
                _logger.LogInformation("AddPaper {namePaper}: {price}", namePaper, price);
            }
        }
    }
}