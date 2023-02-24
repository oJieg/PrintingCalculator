using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase.setting;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{
	public class SettingMashinesController : Controller
	{
		private readonly ApplicationContext _applicationContext;
		private readonly ILogger<SettingMashinesController> _logger;

		public SettingMashinesController(ApplicationContext applicationContext, ILogger<SettingMashinesController> logger)
		{
			_applicationContext = applicationContext;
			_logger = logger;
		}

		public async Task<IActionResult> Index()
		{
			Setting? settings;
			try
			{
				settings = await _applicationContext.Settings.Where(x => x.Id == 1)
				   .Include(x => x.PosMachines)
					   .ThenInclude(x => x.Markups)
				   .Include(x => x.PrintingsMachines)
					   .ThenInclude(x => x.Markups)
				   .Include(x => x.Machines)
					   .ThenInclude(x => x.Markups)
					.Include(x => x.CommonToAllMarkups)
					   .AsNoTracking()
				   .FirstOrDefaultAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка доступа к бд. (index)");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return View("SettingMashines", settings);
		}

		public async Task<IActionResult> EditPrinterSetting(PrintingMachineSetting printingMachineSetting)
		{
			if (printingMachineSetting.ConsumableDye == 0 || String.IsNullOrEmpty(printingMachineSetting.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось изменить PrintingMachineSetting. Не корретные данные.");
			}
			try
			{
				printingMachineSetting.Id = (await _applicationContext.PrintingMachinesSettings
					.Where(x => x.NameMachine == printingMachineSetting.NameMachine)
					.AsNoTracking()
					.FirstAsync()).Id;

				printingMachineSetting.Markups = (await _applicationContext.PrintingMachinesSettings
					.Where(x => x.NameMachine == printingMachineSetting.NameMachine)
					.Include(x => x.Markups)
					.AsNoTracking()
					.FirstAsync()).Markups;

				_applicationContext.Update(printingMachineSetting);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка доступа к бд. (EditPrinterSetting)");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return new RedirectResult("/SettingMashines");
		}

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

			return new RedirectResult("/SettingMashines");
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

			return new RedirectResult("/SettingMashines");
		}
		public async Task<IActionResult> AddMarkup(MarkupAndName markupaAndName)
		{
			if (String.IsNullOrEmpty(markupaAndName.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось добавить Markup. Нет имени.");
			}

			try
			{
				MachineSetting machineSetting = await _applicationContext.MachineSettings
					.Where(x => x.NameMachine == markupaAndName.NameMachine)
					.Include(x => x.Markups)
					.FirstAsync();

				machineSetting.Markups.Add(new Markup()
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

			return new RedirectResult("/SettingMashines");
		}

		public async Task<IActionResult> EdetMashines(MachineSetting machineSetting)
		{
			if (machineSetting.NameMachine == null)
			{
				return ErroMessageForEmptyName("Не удалось изменить MachineSetting. Нет имени.");
			}

			try
			{
				machineSetting.Id = (await _applicationContext.MachineSettings
					.Where(x => x.NameMachine == machineSetting.NameMachine)
					.AsNoTracking()
					.FirstAsync()).Id;

				machineSetting.Markups = (await _applicationContext.MachineSettings
					.Where(x => x.NameMachine == machineSetting.NameMachine)
					.AsNoTracking()
					.FirstAsync()).Markups;

				_applicationContext.Update(machineSetting);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд(EdetMashines)");
				return View("Error", new string("Ошибка доступа к бд"));
			}

			return new RedirectResult("/SettingMashines");
		}
		public async Task<IActionResult> EdetPosMashines(PosMachinesSetting posMachinesSetting)
		{
			if (String.IsNullOrEmpty(posMachinesSetting.NameMachine))
			{
				return ErroMessageForEmptyName("Не удалось изменить PosMachinesSetting. Нет имени.");
			}

			try
			{
				posMachinesSetting.Id = (await _applicationContext.MachineSettings
					.Where(x => x.NameMachine == posMachinesSetting.NameMachine)
					.AsNoTracking()
					.FirstAsync()).Id;

				posMachinesSetting.Markups = (await _applicationContext.MachineSettings
					.Where(x => x.NameMachine == posMachinesSetting.NameMachine)
					.AsNoTracking()
					.FirstAsync()).Markups;

				_applicationContext.Update(posMachinesSetting);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд(PosMachinesSetting)");
				return View("Error", new string("Ошибка доступа к бд"));

			}

			return new RedirectResult("/SettingMashines");
		}
		public async Task<IActionResult> DeleteCommonToAllMarkup(int Id)
		{
			CommonToAllMarkup commonToAllMarkup;
			try
			{
				commonToAllMarkup = await _applicationContext.CommonToAllMarkups
					.Where(x => x.Id == Id).FirstAsync();
				_applicationContext.Remove(commonToAllMarkup);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд(DeleteCommonToAllMarkup)");
				return View("Error", new string("Ошибка доступа к бд"));
			}
			return new RedirectResult("/SettingMashines");
		}

		public async Task<IActionResult> AddCommonToAllMarkup(CommonToAllMarkup commonToAllMarkup)
		{
			if ((commonToAllMarkup.Adjustmen == 0 & commonToAllMarkup.Adjustmen == 0)
				|| commonToAllMarkup.Name == null
				|| commonToAllMarkup.Description == null)
			{
				return ErroMessageForEmptyName("Не удалось добавить CommonToAllMarkup. Добавьте хотя бы одно значение и имя.");
			}

			commonToAllMarkup.Id = null;
			try
			{
				Setting setting = await _applicationContext.Settings
					.Where(x => x.Id == 1)
					.Include(x => x.CommonToAllMarkups)
					.FirstAsync();

				setting.CommonToAllMarkups.Add(commonToAllMarkup);
				await _applicationContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "ошибка доступа к бд(DeleteCommonToAllMarkup)");
				return View("Error", new string("Ошибка доступа к бд"));
			}
			return new RedirectResult("/SettingMashines");
		}

		private IActionResult ErroMessageForEmptyName(string errorMessageForLog)
		{
			_logger.LogError(errorMessageForLog);
			return View("Error", "не кооретные входящие данные");
		}
	}
}
