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

            return true;
        }
    }
}

public class EditPaper
{
    public int id { get; set; }
    public float newPrice { get; set; }
    public int status { get; set; }
}