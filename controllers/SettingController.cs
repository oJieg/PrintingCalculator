using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.Singletones;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingStore _settingStore;
        private readonly ILogger<SettingController> _logger;

        public SettingController(ISettingStore settingStore, ILogger<SettingController> logger)
        {
            _settingStore = settingStore;
            _logger = logger;
        }

        public async Task<IActionResult> Paper()
        {
            PaperAndSize paperAndSize = new();

            try
            {
                var setting = await _settingStore.GetSettings();

                paperAndSize.PaperCatalog = setting.PaperCatalog.OrderBy(x=>x.Id).Where(x=>x.Status>=0).ToList();
                paperAndSize.Size = setting.PaperSizes.ToList();

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
                var setting = await _settingStore.GetCloneSetting();
                if (setting.PaperSizes.Any(size => size.Name == newSizePaper.Name))
                {
                    return new RedirectResult("/Setting/Paper");
                }
                setting.PaperSizes = setting.PaperSizes.Concat(new[] { newSizePaper }).ToArray();

                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetSettings();
                 laminations =  setting.Laminations
                    .Where(l => l.Status >= 0)
                    .OrderBy(l => l.Id)
                    .ToList();
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
                var setting = await _settingStore.GetSettings();
                actualPrice = setting.PrintingsMachine.ConsumablePrice;

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
                var setting = await _settingStore.GetCloneSetting();
                setting.PrintingsMachine.ConsumablePrice = newConsumable;

                await _settingStore.SaveSettings(setting);

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
                var setting = await _settingStore.GetSettings();
                springBrochureSetting = setting.SpringBrochureSettings
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
            var setting = await _settingStore.GetCloneSetting();
            var springBrochure = setting.SpringBrochureSettings.FirstOrDefault(x => x.Id == springBrochureSetting.Id);
            if (springBrochure == null)
            {
                springBrochureSetting.Id = setting.SpringBrochureSettings.Max(x => x.Id) + 1;
                setting.SpringBrochureSettings = setting.SpringBrochureSettings.Concat(new[] { springBrochureSetting }).ToArray();
            }
            else
            {
                springBrochure = springBrochureSetting;

            }

            await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                MachineSetting[] mashines = new MachineSetting[] { setting.PrintingsMachine }.Concat(setting.PosMachines).ToArray();
                MachineSetting mashine = mashines.First(x => x.NameMachine == markupaAndName.NameMachine);

                Markup markup = mashine.Markups.First(x => x.Page == markupaAndName.Page);
                markup = markupaAndName;

                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                MachineSetting[] mashines = new MachineSetting[] { setting.PrintingsMachine }.Concat(setting.PosMachines).ToArray();
                MachineSetting mashine = mashines.First(x => x.NameMachine == markupaAndName.NameMachine);

                Markup markup = mashine.Markups.First(x => x.Page == markupaAndName.Page);

                mashine.Markups.Remove(markupaAndName);
                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                SpringBrochureSetting[] mashines = setting.SpringBrochureSettings.ToArray();
                SpringBrochureSetting mashine = mashines.First();

                mashine.SpringPrice.Add(markupaAndName);

                await _settingStore.SaveSettings(setting);
				//SpringBrochureSetting machineSetting = await _settingStore.SpringBrochureSettings
				//	.Where(x => x.Id == 1)
				//	.Include(x => x.SpringPrice)
				//	.FirstAsync();

				//machineSetting.SpringPrice.Add(new Markup()
				//{
				//	MarkupForThisPage = markupaAndName.MarkupForThisPage,
				//	Page = markupaAndName.Page
				//});

				//await _settingStore.SaveChangesAsync();
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
