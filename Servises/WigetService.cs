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
        private readonly ISettingStore _settingStore;
        private readonly IBitrixWithAauthApi _bitrixApi;
        private readonly ITokenStore _tokenStore;

        private const string INPUT_UF = "ufCrm_1774865963554"; // todo в конфиги

        private readonly ILogger<CalculatorController> _logger;

        public WigetService(IBitrixWithAauthApi bitrixApi, 
            ILogger<CalculatorController> logger,
            ISettingStore settingStore,
            ITokenStore tokenStore)
        {
            _bitrixApi = bitrixApi; //todo поменять на версию с токеном
            _settingStore = settingStore;
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

                int counter = 1;
                foreach (var resultItem in result) {
                    resultItem.Name = $@"{resultItem.Whidth}x{resultItem.Height}, {resultItem.Amount}x{resultItem.Kinds}, {resultItem.Paper}, {resultItem.Price}руб.";
                    resultItem.Id = counter;
                    counter++;
                }

                settingsInfoForWigetCalculation.Inputs = result;
            }
            catch(Exception ex) {
                _logger.LogWarning($"Ошибка десериалищации поля result {ex.Message}", ex);
            }

            return settingsInfoForWigetCalculation;
        }

        private async Task GetSettingsMashines(SettingsInfoForWigetCalculation settingsInfoForWigetCalculation)
        {
            try
            {
                var setting = await _settingStore.GetSettings();

                settingsInfoForWigetCalculation.Paper = setting.PaperCatalog
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToList();
                settingsInfoForWigetCalculation.Lamination = setting.Laminations
                    .Where(lamination => lamination.Status > 0)
                    .OrderBy(lamunation => lamunation.Id)
                    .ToList();
                settingsInfoForWigetCalculation.commonToAllMarkups = setting.CommonToAllMarkups
                    .ToList();
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
