using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.RequestModels;
using Refit;

namespace printing_calculator.Clients
{
    public interface IBitrixWithAauthApi
    {
        /// <summary>
        /// по номеру сделки получить все поля
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/crm.item.get")]
        Task<GetFieldDealAnswer> GetFielDeal(GetFieldDealWithAuthRequest request);
    }
}
