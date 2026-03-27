using Microsoft.AspNetCore.Mvc;
using printing_calculator.Clients;
using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.DTO;
using printing_calculator.ViewModels;

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
        public async Task SetFieldCrm([FromQuery]float price, Input input)
        {
            await _bitrixApi.UpdateCrmDetal(new CrmDealUpdate()
            {
                entityTypeId = 2,
                Id = 57,
                Fields = new FieldsDetailUpdate()
                {
                    opportunity = price
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
            return test;
        }
    }
}
