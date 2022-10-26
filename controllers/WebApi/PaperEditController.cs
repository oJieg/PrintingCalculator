using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;

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
                if(_applicationContext.PaperCatalogs.Any(x => x.Name == paper.Name))
                {
                    PaperCatalog editPaper = _applicationContext.PaperCatalogs
                        .Where(x => x.Name == paper.Name)
                        .First();
                    editPaper.Prices = paper.Price;
                    editPaper.Size = _applicationContext.SizePapers
                        .Where(size => size.Name == paper.NameSize)
                        .First();
                    editPaper.Status = 1;

                    await _applicationContext.SaveChangesAsync();
                    return true;
                }
                PaperCatalog addPaper = new()
                {
                    Name = paper.Name,
                    Prices = paper.Price,                                      
                    Size = _applicationContext.SizePapers
                    .Where(size => size.Name == paper.NameSize)
                    .First(),
                    Status = 1
                };
                _applicationContext.PaperCatalogs.Add(addPaper);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("ошибка добавления новой бумаги", ex);
                return false;
            }
            return true;
        }

        [HttpPut]
        public async Task<bool> Put(EditPaper test)
        {
            PaperCatalog paper;
            try
            {
                paper = _applicationContext.PaperCatalogs
                     .Where(paper => paper.Id == test.id)
                     .First();
            }
            catch (Exception ex)
            {
                _logger.LogError("не удалось получить бумагу", ex);
                return false;
            }

            if (test.status == -99 && test.newPrice >= 0)
            {
                paper.Prices = test.newPrice;
                try
                {
                    await _applicationContext.SaveChangesAsync();
                }
                catch
                {
                    _logger.LogError("не удалось поменять цену бумаги");
                    return false;
                }

                return true;
            }

            if (test.status >= 0)
            {
                if (paper.Status == 0)
                    paper.Status = 1;
                else paper.Status = 0;
                try
                {
                    await _applicationContext.SaveChangesAsync();
                }
                catch
                {
                    _logger.LogError("не удалось поменять статус бумаги");
                    return false;
                }
                return true;
            }

            return false;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
              PaperCatalog paperDelete = _applicationContext.PaperCatalogs
                    .Where(x=>x.Id == id)
                    .First();
                paperDelete.Status = -1;
                await _applicationContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("не удалось удалить бумагу", ex);
                return false;
            }
            
        }
    }
}

public class EditPaper
{
    public int id { get; set; }
    public float newPrice { get; set; }
    public int status { get; set; }
}
public class AddPaper
{
    public string Name { get; set; }
    public float Price { get; set; }
    public string NameSize { get; set; }
}