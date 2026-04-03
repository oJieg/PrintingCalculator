using printing_calculator.Clients.AnswerModels;
using printing_calculator.Clients.DTO;
using printing_calculator.Clients.RequestModels;
using printing_calculator.ViewModels.Result;
using Refit;

namespace printing_calculator.Clients
{
    public interface IBitrixApi
    {
        /// <summary>
        /// Изменить поля в карточке.
        /// </summary>
        /// <param name="crmDealUpdate"></param>
        /// <returns></returns>
        [Post("/crm.item.update")]
        Task UpdateCrmDetal(CrmDealUpdate crmDealUpdate);

        [Post("/crm.item.productrow.add")]
        Task AddProduct(AddProductRequest addProductRequest);

        /// <summary>
        /// получить все созданые поля для всех карточек(название-тип)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/crm.item.fields")]
        Task<GetFieldsAnswer> GetFields(GetFieldsRequest request);

        /// <summary>
        /// по номеру сделки получить все поля
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/crm.item.get")]
        Task<GetFieldDealAnswer> GetFielDeal(GetFieldDealRequest request);
    }

    //crm.item.list - получить все поля - имя - title {"entityTypeId":2}
    //crm.item.get - по номеру сделки(карточки) получить все поля {"entityTypeId":2,"id":57}
}

