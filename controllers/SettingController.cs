using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;

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

        public async Task<IActionResult> SpringBrochureSetting()
        {
			SpringBrochureSetting springBrochureSetting = new();

			try
			{
                springBrochureSetting = _applicationContext.SpringBrochureSettings
                    .Include(x=>x.SpringPrice)
                    .First();

			}
			catch (Exception ex)
			{
				_logger.LogError("error db", ex);
				return NotFound();
			}

			return View("SpringBrochureSetting", springBrochureSetting);
		}

        public async Task<IActionResult> EditSpringBrochureSetting(SpringBrochureSetting springBrochureSetting)
        {
            springBrochureSetting.SpringPrice = (await _applicationContext.SpringBrochureSettings
                .Where(x => x.Id == springBrochureSetting.Id)
                .Include(x => x.SpringPrice)
                .AsNoTracking()
                .FirstAsync()).SpringPrice;

            _applicationContext.Update(springBrochureSetting);
            await _applicationContext.SaveChangesAsync();
            return new RedirectResult("/Setting/SpringBrochureSetting");
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

        //да да, дублирование кода...
		public async Task<IActionResult> EditMarkup(MarkupAndName markupaAndName)
		{
			if (String.IsNullOrEmpty(markupaAndName.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось изменить Markup. Нет имени.");
			}
			try
			{
				_applicationContext.Update((Markup)markupaAndName);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка доступа к бд. (EditMarkup)");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return new RedirectResult("/Setting/SpringBrochureSetting");
		}

		public async Task<IActionResult> DelMarkup(MarkupAndName markupaAndName)
		{
			if (String.IsNullOrEmpty(markupaAndName.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось удалить Markup. Нет имени.");
			}
			try
			{
				_applicationContext.Remove((Markup)markupaAndName);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд. DelMarkup");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return new RedirectResult("/Setting/SpringBrochureSetting");
		}
		public async Task<IActionResult> AddMarkup(MarkupAndName markupaAndName)
		{
			if (String.IsNullOrEmpty(markupaAndName.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось добавить Markup. Нет имени.");
			}

			try
			{
				SpringBrochureSetting machineSetting = await _applicationContext.SpringBrochureSettings
					.Where(x => x.Id == 1)
					.Include(x => x.SpringPrice)
					.FirstAsync();

				machineSetting.SpringPrice.Add(new Markup()
				{
					MarkupForThisPage = markupaAndName.MarkupForThisPage,
					Page = markupaAndName.Page
				});

				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд(AddMarkup)");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return new RedirectResult("/Setting/SpringBrochureSetting");
		}
		private IActionResult ErroMessageForEmptyName(string errorMessageForLog)
		{
			_logger.LogError(errorMessageForLog);
			return View("Error", "не кооретные входящие данные");
		}
	}
}
