using Microsoft.AspNetCore.Mvc;
using printing_calculator.Clients;
using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.DTO;
using printing_calculator.ViewModels;
using System.Text.Json;
using printing_calculator.ViewModels.Result;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class IntegrationApiController : ControllerBase
    {
        public IBitrixApi _bitrixApi;
        public IntegrationApiController(IBitrixApi bitrixApi) {
            _bitrixApi = bitrixApi;
        }
        [HttpPut("api/set-field-crm")]
        public async Task SetFieldCrm([FromQuery]float price, CalculationResult result)
        {

            await _bitrixApi.UpdateCrmDetal(new CrmDealUpdate()
            {
                entityTypeId = 2,
                Id = 91,
                Fields = new FieldsDetailUpdate()
                {
                    opportunity = price,
                    DynamicFields = new Dictionary<string, object>() 
                    { 
                        ["ufCrm_1774610816585"] = JsonSerializer.Serialize(result)
                    }
                }
            });
        }

        [HttpPost("api/test2")]
        public async Task<GetFieldDealAnswer> Test2(int id)
        {
           var test =  await _bitrixApi.GetFielDeal(new Clients.RequestModels.GetFieldDealRequest()
           {
               Id = id
           });

            await _bitrixApi.UpdateCrmDetal(new CrmDealUpdate()
            {
                entityTypeId = 2,
                Id = id,
                Fields = new FieldsDetailUpdate()
                {
                    opportunity = 100500,
                    DynamicFields = new Dictionary<string, object>()
                    {
                        ["ufCrm_1774349835977"] = "test",
                        ["ufCrm_1774610816585"] = JsonSerializer.Serialize(new ApiResultAnswer())
                    }
                }
            });

            return test;
        }
    }
}
