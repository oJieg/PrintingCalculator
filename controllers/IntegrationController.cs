using Microsoft.AspNetCore.Mvc;
using printing_calculator.Clients;
using printing_calculator.controllers.WebApi.RequestModels;
using printing_calculator.ViewModels;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


namespace printing_calculator.controllers
{

    public class IntegrationController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBitrixApi _bitrixApi;
        private readonly ILogger<CalculatorController> _logger;


        public IntegrationController(IBitrixApi bitrixApi, ILogger<CalculatorController> logger, ApplicationContext applicationContext) { 
            _bitrixApi = bitrixApi;
            _applicationContext = applicationContext;
            _logger = logger;
        }

        [HttpPost("api/test")]
        public async Task<IActionResult> Index([FromForm] TestRequest testRequest)
        {
            string elementId = null;
            if (!string.IsNullOrEmpty(testRequest.PLACEMENT_OPTIONS))
            {
                try
                {
                    var options = JsonSerializer.Deserialize<Dictionary<string, string>>(testRequest.PLACEMENT_OPTIONS);
                    if (options != null && options.TryGetValue("ID", out string id))
                    {
                        elementId = id;
                    }
                }
                catch (JsonException)
                {
                    // Невалидный JSON
                }
            }

            PaperAndHistoryInput PaperAndHistoryInput = new();
            try
            {
                PaperAndHistoryInput.Paper = await _applicationContext.PaperCatalogs
                    .Include(paper => paper.Size)
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToListAsync();
                PaperAndHistoryInput.Lamination = await _applicationContext.Laminations
                    .Where(lamination => lamination.Status > 0)
                    .OrderBy(lamunation => lamunation.Id)
                    .ToListAsync();
                PaperAndHistoryInput.commonToAllMarkups = await _applicationContext.CommonToAllMarkups
                    .ToListAsync();

            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не вышло получить из базы PaperCatalogs " +
                    "и laminations");
            }

            //тут читаем поля и добавляем в PaperAndHistoryInput(переименовать)

            return View("WidgetCalculator", PaperAndHistoryInput);


            await _bitrixApi.UpdateCrmDetal(new Clients.DTO.CrmDealUpdate()
            {
                entityTypeId = 2,
                Id = int.Parse(elementId),
                Fields = new Clients.DTO.FieldsDetailUpdate()
                {
                    opportunity = 9999,
                    title = "test2"
                }
               // auth = testRequest.AUTH_ID
            });
        }
    }
}
