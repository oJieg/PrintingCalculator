using printing_calculator.controllers.WebApi.RequestModels;
using printing_calculator.ViewModels;

namespace printing_calculator.Servises.Interface
{
    public interface IWigetService
    {
        Task<SettingsInfoForWigetCalculation> GetVievModelForWiget(CrmPlasmentRequest crmPlasmentRequest);
    }
}
