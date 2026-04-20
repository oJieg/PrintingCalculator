using printing_calculator.DataBase.setting;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels;

namespace printing_calculator.Models
{
    public class Validation
    {
        private readonly ISettingStore _applicationContext;
        private PrintingMachineSetting? _printingMachineSetting;

        public Validation(ISettingStore applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<bool> TryValidateInputAsync(Input input, CancellationToken cancellationToken)
        {
            var setting = await _applicationContext.GetSettings();
            _printingMachineSetting = setting.PrintingsMachine;
            if (_printingMachineSetting == null)
            {
                return false;
            }

            return input != null &&
                TryValidationSize(input.Whidth) &&
                TryValidationSize(input.Height) &&
                await TryValidationNamePaperAsync(input.Paper) &&
                IsPositiveNumber(input.Amount) &&
                IsPositiveNumber(input.Kinds) &&
                await TryValidationLaminationName(input.LaminationName) &&
                TryValidationPos(input.Creasing) &&
                TryValidationPos(input.Drilling);
        }

        private bool TryValidationSize(int size)
        {

            return size > 0 && size < _printingMachineSetting.MaximumSizeLength;
        }

        private async Task<bool> TryValidationNamePaperAsync(string namePaper)
        {
            try
            {
                var setting = await _applicationContext.GetSettings();

                return setting.PaperCatalog
                    .Any(paperCatalogs => paperCatalogs.Name == namePaper);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        private async Task<bool> TryValidationLaminationName(string? nameLamination)
        {
            if (nameLamination != null && nameLamination != string.Empty)
            {
                try
                {
                    var setting = await _applicationContext.GetSettings();

                    return setting.Laminations
                        .Any(laminations => laminations.Name == nameLamination);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsPositiveNumber(int number)
        {
            return number > 0;
        }

        private bool TryValidationPos(int countPos)
        {
            return countPos == 0 || (countPos > 0 && countPos < 50);
        }
    }
}
