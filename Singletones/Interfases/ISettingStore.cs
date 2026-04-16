using printing_calculator.Models;

namespace printing_calculator.Singletones.Interfases
{
    public interface ISettingStore
    {
        Task<SettingCalculation> GetSettings(CancellationToken cancellationToken = default);
        Task SaveSettings(SettingCalculation setting, CancellationToken cancellationToken = default);
    }
}
