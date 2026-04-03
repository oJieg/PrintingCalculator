using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Clients;
using printing_calculator.controllers;
using printing_calculator.controllers.WebApi.RequestModels;
using printing_calculator.Servises.Interface;
using printing_calculator.ViewModels;
using System.Text.Json;
using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.RequestModels;
using printing_calculator.Singletones.Interfases;
using printing_calculator.Exceptions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace printing_calculator.Servises
{
    public class WigetService: IWigetService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IBitrixWithAauthApi _bitrixApi;
        private readonly ITokenStore _tokenStore;

        private const string INPUT_UF = "ufCrm_1774865963554"; // todo в конфиги

        private readonly ILogger<CalculatorController> _logger;

        public WigetService(IBitrixWithAauthApi bitrixApi, 
            ILogger<CalculatorController> logger, 
            ApplicationContext applicationContext,
            ITokenStore tokenStore)
        {
            _bitrixApi = bitrixApi;
            _applicationContext = applicationContext;
            _tokenStore = tokenStore;
            _logger = logger;
        }

        public async Task<SettingsInfoForWigetCalculation> GetVievModelForWiget(CrmPlasmentRequest crmPlasmentRequest)
        {
            int dealId = DeserializePlacementOptionForDeal(crmPlasmentRequest);

            SettingsInfoForWigetCalculation settingsInfoForWigetCalculation = new();
            await GetSettingsMashines(settingsInfoForWigetCalculation);

            settingsInfoForWigetCalculation.DealId = dealId;
            settingsInfoForWigetCalculation.Token = _tokenStore.CreateNewToken(dealId, crmPlasmentRequest.AuthorizationId); //todo генерирует новый токен

            GetFieldDealAnswer fieldDeal = new();
            try
            {
                fieldDeal = await _bitrixApi.GetFielDeal(new GetFieldDealWithAuthRequest()
                {
                    EntityTypeId = 2,
                    Id = dealId,
                    auth = crmPlasmentRequest.AuthorizationId
                });
            }
            catch (Refit.ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new ApiException(ex.StatusCode, "Ошибка авторизации виджета");
                }

                throw new ApiException(ex.StatusCode, "Ошибка CRM");
            }

            try
            {
                JsonElement historyInput = (JsonElement)fieldDeal.Result.Item[INPUT_UF];

                string[]? jsonStrings = JsonSerializer.Deserialize<string[]>(historyInput.GetRawText());

                InputForWiget[] result = jsonStrings.Select(jsonString => JsonSerializer.Deserialize<InputForWiget>(jsonString)).ToArray();

                settingsInfoForWigetCalculation.Inputs = result;
            }
            catch { 
            
            }
            //var x = JsonSerializer.Deserialize<InputForWiget[]>(historyInput.GetRawText());
            //тут читаем поля и добавляем в PaperAndHistoryInput(переименовать)

            return settingsInfoForWigetCalculation;
        }

        private async Task GetSettingsMashines(SettingsInfoForWigetCalculation settingsInfoForWigetCalculation)
        {
            try
            {
                settingsInfoForWigetCalculation.Paper = await _applicationContext.PaperCatalogs
                    .Include(paper => paper.Size)
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToListAsync();
                settingsInfoForWigetCalculation.Lamination = await _applicationContext.Laminations
                    .Where(lamination => lamination.Status > 0)
                    .OrderBy(lamunation => lamunation.Id)
                    .ToListAsync();
                settingsInfoForWigetCalculation.commonToAllMarkups = await _applicationContext.CommonToAllMarkups
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не вышло получить из базы PaperCatalogs " +
                    "и laminations");
            }
        }

        private int DeserializePlacementOptionForDeal(CrmPlasmentRequest crmPlasmentRequest)
        {
            try
            {
                Dictionary<string, string>? options = JsonSerializer.Deserialize<Dictionary<string, string>>(crmPlasmentRequest.PlacementOptions);
                if (options != null && options.TryGetValue("ID", out string id))
                {
                   return int.Parse(options["ID"]);
                }
                else
                {
                    _logger.LogError("ошибка десериализации, отсусттвует ключ  ID у PLACEMENT_OPTIONS: {0}", crmPlasmentRequest.PlacementOptions);
                    throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Не удалось десериализовать PLACEMENT_OPTIONS, не найден ID для сделки");
                }
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Ошибка валидации json из поля PLACEMENT_OPTIONS");
                throw new ApiException(System.Net.HttpStatusCode.BadRequest, "Не удалось десериализовать PLACEMENT_OPTIONS");
            }
        }
    }
}
