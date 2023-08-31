using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace printing_calculator.controllers.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaperEditController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<PaperEditController> _logger;

        public PaperEditController(ApplicationContext applicationContext,
            ILogger<PaperEditController> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        [HttpPost]
        public async Task<bool> Post(AddPaper paper)
        {
            try
            {
                if (_applicationContext.PaperCatalogs.Any(x => x.Name == paper.Name))
                {
                    PaperCatalog editPaper = await _applicationContext.PaperCatalogs
                        .Where(x => x.Name == paper.Name)
                        .FirstAsync();
                    editPaper.Prices = paper.Price;
                    editPaper.Size = await _applicationContext.SizePapers
                        .Where(size => size.Name == paper.NameSize)
                        .FirstAsync();
                    editPaper.Status = 1;

                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                PaperCatalog addPaper = new()
                {
                    Name = paper.Name,
                    Prices = paper.Price,
                    Size = await _applicationContext.SizePapers
                    .Where(size => size.Name == paper.NameSize)
                    .FirstAsync(),
                    Status = 1
                };
                _applicationContext.PaperCatalogs.Add(addPaper);
                await _applicationContext.SaveChangesAsync();
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
                paper = await _applicationContext.PaperCatalogs
                     .Where(paper => paper.Id == editPaper.id)
                     .FirstAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не удалось получить бумагу при попытки изменении");
                return false;
            }

            if (editPaper.status == -99 && editPaper.newPrice >= 0)
            {
                paper.Prices = editPaper.newPrice;
                paper.PaperThickness = editPaper.PaperThickness;
                try
                {
                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    _logger.LogError("не удалось поменять цену бумаги");
                    return false;
                }
            }

            if (editPaper.status >= 0)
            {
                if (paper.Status == 0)
                    paper.Status = 1;
                else paper.Status = 0;
                try
                {
                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    _logger.LogError("не удалось поменять статус бумаги");
                    return false;
                }
            }

            return false;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                PaperCatalog paperDelete = await _applicationContext.PaperCatalogs
                      .Where(x => x.Id == id)
                      .FirstAsync();
                paperDelete.Status = -1;
                await _applicationContext.SaveChangesAsync();
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