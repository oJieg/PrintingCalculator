using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    public class SettingController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<SettingController> _logger;

        public SettingController(ApplicationContext applicationContext, ILogger<SettingController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<IActionResult> Paper()
        {
            PaperAndSize paperAndSize = new();

            try
            {
                paperAndSize.PaperCatalog = await _applicationContext.PaperCatalogs
                    .Include(paper => paper.Size)
                    .OrderBy(paper => paper.Id)
                    .Where(paper => paper.Status >= 0)
                    .AsNoTracking()
                    .ToListAsync();
                paperAndSize.Size = await _applicationContext.SizePapers
                    .AsNoTracking()
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError("error db", ex);
                return NotFound();
            }

            return View("SettingPaper", paperAndSize);
        }

        public async Task<IActionResult> AddSizePaper(SizePaper newSizePaper)
        {
            if (ValidationSize(newSizePaper))
            {
                return BadRequest();
            }
            newSizePaper.Name += newSizePaper.Height.ToString() + "x" + newSizePaper.Width.ToString(); 
            try
            {
                if (await _applicationContext.SizePapers.AnyAsync(size => size.Name == newSizePaper.Name))
                {
                    return new RedirectResult("/Setting/Paper");
                }
                _applicationContext.SizePapers.Add(newSizePaper);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "неудалось добавить новый размер");
                return new RedirectResult("/Setting/Paper");
            }
            return new RedirectResult("/Setting/Paper");
        }

        public async Task<IActionResult> Lamination()
        {
            List<Lamination> laminations;
            try
            {
                 laminations = await _applicationContext.Laminations
                    .Where(l => l.Status >= 0)
                    .OrderBy(l => l.Id)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ошибка чтения списка ламинаций");
                return NotFound();
            }

            return View("SettingLamination", laminations);
        }

        public async Task<IActionResult> Consumable()
        {
            ConsumablePrice actualPrice;
            try
            {
                actualPrice = await _applicationContext.ConsumablePrices
                   .OrderBy(x => x.Id)
                   .LastAsync();
                return View("SettingConsumables", actualPrice);
            }
            catch
            {
                _logger.LogError("ошибка чтения ConsumablePrice");
                return NotFound();
            }
        }

        public async Task<IActionResult> EditConsumable(ConsumablePrice newConsumable)
        {
            if(ValidationConsumable(newConsumable))
            {
                return NotFound();
            }
            try
            {
                _applicationContext.ConsumablePrices.Add(newConsumable);
                await _applicationContext.SaveChangesAsync();
                return RedirectToAction("Consumable");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось изменить Consumable");
                return NotFound();
            }
        }

        private bool ValidationConsumable(ConsumablePrice newConsumable)
        {
            return newConsumable.DrumPrice1 <= 0
                && newConsumable.DrumPrice2 <= 0
                && newConsumable.DrumPrice3 <= 0
                && newConsumable.DrumPrice4 <= 0
                && newConsumable.TonerPrice <= 0;
        }
        private bool ValidationSize(SizePaper newSizePaper)
        {
            return newSizePaper.Height <= 100
            && newSizePaper.Width <= 100;
        }
    }
}
