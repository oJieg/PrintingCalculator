using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.DataBase;
using printing_calculator.DataBase.setting;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{
    public class SettingMashinesController : Controller
    {
        private readonly ISettingStore _settingStore;
        private readonly ILogger<SettingMashinesController> _logger;

        public SettingMashinesController(ISettingStore settingStore, ILogger<SettingMashinesController> logger)
        {
            _settingStore = settingStore;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            Setting? settings;
            var setting = await _settingStore.GetSettings();
            try
            {
                settings = new()
                {
                    PosMachines = setting.PosMachines,
                    CommonToAllMarkups = setting.CommonToAllMarkups,
                    Machines = setting.PosMachines,
                    PrintingsMachines = setting.PrintingsMachine,

                };
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
                var setting = await _settingStore.GetCloneSetting();
                setting.PrintingsMachine = printingMachineSetting;

                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                MachineSetting[] mashines = new MachineSetting[] { setting.PrintingsMachine }.Concat(setting.PosMachines).ToArray();
                MachineSetting mashine = mashines.First(x => x.NameMachine == markupaAndName.NameMachine);

                mashine.Markups.Add(markupaAndName);

                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                MachineSetting[] mashines = new MachineSetting[] { setting.PrintingsMachine }.Concat(setting.PosMachines).ToArray();
                MachineSetting mashine = mashines.First(x => x.NameMachine == machineSetting.NameMachine);

                mashine = machineSetting;

                await _settingStore.SaveSettings(setting);
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
                var setting = await _settingStore.GetCloneSetting();
                MachineSetting[] mashines = new MachineSetting[] { setting.PrintingsMachine }.Concat(setting.PosMachines).ToArray();
                MachineSetting mashine = mashines.First(x => x.NameMachine == posMachinesSetting.NameMachine);

                mashine = posMachinesSetting;

                await _settingStore.SaveSettings(setting);
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

                var setting = await _settingStore.GetCloneSetting();
                setting.CommonToAllMarkups = setting.CommonToAllMarkups.Where(x=>x.Id !=  Id).ToArray();

                await _settingStore.SaveSettings(setting);
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
            if (commonToAllMarkup.Name == null
                || commonToAllMarkup.Description == null)
            {
                return ErroMessageForEmptyName("Не удалось добавить CommonToAllMarkup. Добавьте хотя бы одно значение и имя.");
            }

            commonToAllMarkup.Id = null;
            try
            {
                var setting = await _settingStore.GetCloneSetting();
                commonToAllMarkup.Id = setting.CommonToAllMarkups.Max(x => x.Id) + 1;
                setting.CommonToAllMarkups = setting.CommonToAllMarkups.Concat(new[] { commonToAllMarkup}).ToArray();

                await _settingStore.SaveSettings(setting);
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
