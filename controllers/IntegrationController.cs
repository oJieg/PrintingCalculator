using Microsoft.AspNetCore.Mvc;
using printing_calculator.controllers.WebApi.RequestModels;
using printing_calculator.Servises.Interface;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{

    public class IntegrationController : Controller
    {
        private readonly IWigetService _wigetService;
        private readonly ILogger<CalculatorController> _logger;


        public IntegrationController(ILogger<CalculatorController> logger, IWigetService wigetService)
        {
            _wigetService = wigetService;
            _logger = logger;
        }

        [HttpPost("api/deal_calculator_wiget")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DealCalculatorWiget([FromForm] CrmPlasmentRequest crmPlasmentRequest)
        {

            SettingsInfoForWigetCalculation settigWiget = await _wigetService.GetVievModelForWiget(crmPlasmentRequest);
            return View("WidgetCalculator", settigWiget);
        }
    }
}
