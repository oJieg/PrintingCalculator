using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaminationEditController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<PaperEditController> _logger;

        public LaminationEditController(ApplicationContext applicationContext,
            ILogger<PaperEditController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> Post(AddLamination addLamination)
        {
            try
            {
                Lamination lamination;
                if (_applicationContext.Laminations.Any(x => x.Name == addLamination.Name))
                {
                    lamination = await _applicationContext.Laminations
                        .Where(x => x.Name == addLamination.Name)
                        .FirstAsync();
                    lamination.Price = addLamination.Price;
                    lamination.Status = 1;

                    await _applicationContext.SaveChangesAsync();
                    return true;
                }

                lamination = new Lamination()
                {
                    Name = addLamination.Name,
                    Price = addLamination.Price,
                    Status = 1
                };
                _applicationContext.Laminations.Add(lamination);
                await _applicationContext.SaveChangesAsync();
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
            Lamination lamination;
            try
            {
                lamination = await _applicationContext.Laminations
                    .Where(la => la.Id == editLamination.id)
                    .FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось получить ламинацию для ее изменения");
                return false;
            }

            if (editLamination.status == -99 && editLamination.newPrice >= 0)
            {
                lamination.Price = editLamination.newPrice;
                try
                {
                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    _logger.LogError("не удалось сохранить изменение при попытки изменить ламинацию");
                    return false;
                }
            }

            if (editLamination.status >= 0)
            {
                if (lamination.Status == 0)
                    lamination.Status = 1;
                else lamination.Status = 0;

                try
                {
                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    _logger.LogError("не удалось изменить статус ламинации");
                    return false;
                }
            }
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteLamination(int id)
        {
            try
            {
                Lamination lamination = await _applicationContext.Laminations
                    .Where(la => la.Id == id)
                    .FirstAsync();
                lamination.Status = -1;
                await _applicationContext.SaveChangesAsync();
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
