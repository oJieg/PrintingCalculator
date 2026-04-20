using Microsoft.AspNetCore.Mvc;
using printing_calculator.Clients;
using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.DTO;
using printing_calculator.Clients.RequestModels;
using printing_calculator.DataBase;
using printing_calculator.Exceptions;
using printing_calculator.Models;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels;
using printing_calculator.ViewModels.Result;
using System.Net;
using System.Text.Json;

namespace printing_calculator.controllers.WebApi
{
    [ApiController]
    public class IntegrationApiController : ControllerBase
    {
        public readonly IBitrixWithAauthApi _bitrixApi;
        //private readonly ApplicationContext _applicationContext;
        private const string PRODUCT_UF = "ufCrm_1774601696653"; //TODO вынести в конфиги
        private const string INPUT_UF = "ufCrm_1774865963554";
        private readonly ITokenStore _tokenStore;
        private readonly ISettingStore _settingStore;

        public IntegrationApiController(IBitrixWithAauthApi bitrixApi, ITokenStore tokenStore, ISettingStore settingStore)
        {
            _bitrixApi = bitrixApi;
            _tokenStore = tokenStore;
            _settingStore = settingStore;
            //_applicationContext = context;
        }
        [HttpPut("api/set-field-crm")]
        public async Task<ActionResult<InputForWiget[]>> SetFieldCrm(CalculatorFullResult result)
        {
            var token = _tokenStore.GetDealAutorizationInfo(result.Token);
            if (token.DealId != result.DealId) {
                return Unauthorized();
            }

            foreach (InputForWiget input in result.Inputs)
            {
                if (input.Name == null) 
                {
                    input.Name = $@"{input.Whidth}x{input.Height}, {input.Amount}x{input.Kinds}, {input.Paper}, {input.Price}руб.";
                } 
            }
            

            var fields = new FieldsDetailUpdate()
            {
                opportunity = result.Inputs.Sum(x => x.Price),
                DynamicFields = new Dictionary<string, object>()
                {
                    [INPUT_UF] = result.Inputs.Select(x => JsonSerializer.Serialize(x)).ToArray(),
                    [PRODUCT_UF] =  result.Inputs.Select(x => x.Name).ToArray(),
                }
            };
            try
            {
                await _bitrixApi.UpdateCrmDetal(new CrmDealUpdateWithAuthRequest()
                {
                    entityTypeId = 2,
                    Id = result.DealId,
                    Fields = fields,
                    auth = token.BitrixAuthToken
                });
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("Токен авторизации не корректный");
            }
            catch (ApiException ex)
            {
                return StatusCode(500, "Ошибка crm");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ошибка соеденения ");
            }

            return result.Inputs;
        }

        public class CalculatorFullResult
        {
            public InputForWiget[] Inputs { get; set; }
            public int DealId { get; set; }
            public string Token { get; set; }
        }

        [HttpPost("api/test2")]
        public async Task Test2()
        {

            //await _settingStore.SaveSettings( SettingCalculationGenerator.GenerateRandomSettingCalculation());
            ISettingCalculation x = await _settingStore.GetSettings();
            return;
        }

    }
}
