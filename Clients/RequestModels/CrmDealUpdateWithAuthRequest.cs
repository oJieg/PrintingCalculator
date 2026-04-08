using printing_calculator.Clients.DTO;

namespace printing_calculator.Clients.RequestModels
{
    public class CrmDealUpdateWithAuthRequest : CrmDealUpdate
    {
        public string auth { get; set; }
    }
}
