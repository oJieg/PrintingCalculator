using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Singletones.Interfases;

namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaminationEditController : ControllerBase
    {
        private readonly ISettingStore _settingStore;
        private readonly ILogger<PaperEditController> _logger;

        public LaminationEditController(ISettingStore settingStore,
            ILogger<PaperEditController> logger)
        {
            _settingStore = settingStore;
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> Post(AddLamination addLamination)
        {
            try
            {
                var setting = await _settingStore.GetCloneSetting();

                setting.Laminations = setting.Laminations.Concat(new[]
                {
                    new Lamination
                    {
                        Id = setting.Laminations.Max(la => la.Id) + 1,
                        Name = addLamination.Name,
                        Price = addLamination.Price,
                        Status = 1
                    }
                }).ToArray();

                await _settingStore.SaveSettings(setting);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ошибка добавлени новой ламинации");
                return false;
            }
        }

        [HttpPut]
        public async Task<bool> Put(EditLamination editLamination)
        {
            try
            {
                var setting = await _settingStore.GetCloneSetting();
                var lamination = setting.Laminations.First(la => la.Id == editLamination.id);

                lamination.Price = editLamination.newPrice;
                lamination.Status = editLamination.status;

                await _settingStore.SaveSettings(setting);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось получить ламинацию для ее изменения");
                return false;
            }
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteLamination(int id)
        {
            try
            {
                var setting = await _settingStore.GetCloneSetting();
                var lamination = setting.Laminations.First(x => x.Id == id);
                lamination.Status = -1;
                
                await _settingStore.SaveSettings(setting);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось удалить ламинацию");
                return false;
            }
        }
    }


    public class AddLamination
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }

    public class EditLamination
    {
        public int id { get; set; }
        public float newPrice { get; set; }
        public int status { get; set; }
    }
}