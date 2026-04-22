using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Singletones.Interfases;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaperEditController : ControllerBase
    {
        private readonly ISettingStore _settingStore;
        private readonly ILogger<PaperEditController> _logger;

        public PaperEditController(ISettingStore settingStore,
            ILogger<PaperEditController> logger)
        {
            _settingStore = settingStore;
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> Post(AddPaper paper)
        {
            try
            {
                var setting = await _settingStore.GetCloneSetting();

                if (setting.PaperCatalog.Any(x => x.Name == paper.Name))
                {
                    var settingPaper = setting.PaperCatalog.First(x => x.Name == paper.Name);
                    settingPaper.Name = paper.Name;
                    settingPaper.Prices = paper.Price;
                    settingPaper.Size = setting.PaperSizes.First(x => x.Name == paper.Name);
                    settingPaper.Status = 1;
                }
                else
                {
                    setting.PaperCatalog = setting.PaperCatalog.Concat(new[]
                    {
                        new PaperCatalog
                        {
                            Id = setting.PaperCatalog.Max(x => x.Id) + 1,
                            Name = paper.Name,
                            Size = setting.PaperSizes.First(x => x.Name == paper.NameSize),
                            Status = 1,
                            PaperThickness = 0,
                            Prices = paper.Price
                        }
                    }).ToArray();
                }

                await _settingStore.SaveSettings(setting);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("ошибка добавления новой бумаги", ex);
                return false;
            }
        }

        [HttpPut]
        public async Task<bool> Put(EditPaper editPaper)
        {
            PaperCatalog paper;
            try
            {
                var setting = await _settingStore.GetCloneSetting();

                var settingPaper = setting.PaperCatalog.First(x => x.Id == editPaper.id);
                settingPaper.Prices = editPaper.newPrice;
                settingPaper.Status = editPaper.status;
                settingPaper.PaperThickness = editPaper.PaperThickness;

                await _settingStore.SaveSettings(setting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось получить бумагу при попытки изменении");
                return false;
            }
            
            return true;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                var setting = await _settingStore.GetCloneSetting();
                var settingPaper = setting.PaperCatalog.First(x => x.Id == id);
                settingPaper.Status = -1;
                
                await _settingStore.SaveSettings(setting);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось удалить бумагу");
                return false;
            }
        }
    }


    public class EditPaper
    {
        public int id { get; set; }
        public float newPrice { get; set; }
        public int status { get; set; }
        public float PaperThickness { get; set; }
    }

    public class AddPaper
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string NameSize { get; set; }
    }
}