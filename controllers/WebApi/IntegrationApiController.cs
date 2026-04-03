using Microsoft.AspNetCore.Mvc;
using printing_calculator.Clients;
using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.DTO;
using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using printing_calculator.ViewModels.Result;
using System.Text.Json;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class IntegrationApiController : ControllerBase
    {
        public IBitrixApi _bitrixApi;
        private const string PRODUCT_UF = "ufCrm_1774601696653";
        private const string INPUT_UF = "ufCrm_1774865963554";
        private const string RESULT_UF = "ufCrm_1774865706945";
        public IntegrationApiController(IBitrixApi bitrixApi) {
            _bitrixApi = bitrixApi;
        }
        [HttpPut("api/set-field-crm")]
        public async Task SetFieldCrm(CalculatorFullResult result)
        {
            var fieldsDeal =  await _bitrixApi.GetFielDeal(new Clients.RequestModels.GetFieldDealRequest()
            {
                Id = result.DealId,
            });

            var products = (JsonSerializer.Deserialize<object[]>(fieldsDeal.Result.Item[PRODUCT_UF].ToString()));

            await _bitrixApi.UpdateCrmDetal(new CrmDealUpdate()
            {
                entityTypeId = 2,
                Id = result.DealId,
                Fields = new FieldsDetailUpdate()
                {
                    opportunity = result.Inputs.Sum(x=>x.Price),
                    DynamicFields = new Dictionary<string, object>() 
                    { 
                        //[RESULT_UF] = new object[] { JsonSerializer.Serialize(result.Result) },
                        [INPUT_UF] =  result.Inputs.Select(x=> JsonSerializer.Serialize(x)).ToArray(),
                        //[PRODUCT_UF] = products.Concat(new object[] { $@"{result.Input.Whidth}x{result.Input.Height}, {result.Input.Amount}x{result.Input.Kinds}, {result.Input.Paper}, {result.Result.Price}руб." })
                    }
                }
            });
        }

        public class CalculatorFullResult
        {
            //public CalculationResult Result {  get; set; }
            public InputForWiget[] Inputs { get; set; }
            public int DealId { get; set; }
            public string Token { get; set; }
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
