using printing_calculator.Models;
using printing_calculator.Singletones.Interfases;
using System.Text.Json;

namespace printing_calculator.Singletones
{
    public class SettingStore : ISettingStore
    {
        private SettingCalculation _settingCalculation;
        private SemaphoreSlim _lock = new(1,1);
        private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settingPrint.json");


        public async Task<SettingCalculation> GetSettings(CancellationToken cancellationToken=default)
        {
            await _lock.WaitAsync(cancellationToken);

            try
            {
                if (_settingCalculation == null)
                {
                    _settingCalculation = await LoadSetting(cancellationToken);
                }
                return CloneSetting(_settingCalculation);
            }
            finally { _lock.Release(); }
        }

        public async Task SaveSettings(SettingCalculation setting, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);

            try
            {
                using FileStream stream = File.Create(_filePath);
                await JsonSerializer.SerializeAsync(stream, setting, _serializerOptions, cancellationToken:cancellationToken);

                _settingCalculation = CloneSetting(setting);
            }
            finally { _lock.Release(); }
        }

        private async Task<SettingCalculation> LoadSetting(CancellationToken cancellationToken )
        {
            using FileStream stream = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<SettingCalculation>(stream, _serializerOptions, cancellationToken)?? new SettingCalculation();
        }

        private SettingCalculation CloneSetting(SettingCalculation setting)
        {
            string json = JsonSerializer.Serialize(setting);
            return JsonSerializer.Deserialize<SettingCalculation>(json);
        }

    }
}
