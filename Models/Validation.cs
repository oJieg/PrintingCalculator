using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models
{
    public class Validation
    {
        private readonly ApplicationContext _applicationContext;
        private PrintingMachineSetting? _printingMachineSetting;

        public Validation(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<bool> TryValidateInputAsync(Input input, CancellationToken cancellationToken)
        {
            _printingMachineSetting = _applicationContext.PrintingMachinesSettings.FirstOrDefault();
            if (_printingMachineSetting == null)
            {
                return false;
            }

            return input != null &&
                TryValidationSize(input.Whidth) &&
                TryValidationSize(input.Height) &&
                await TryValidationNamePaperAsync(input.Paper, cancellationToken) &&
                IsPositiveNumber(input.Amount) &&
                IsPositiveNumber(input.Kinds) &&
                await TryValidationLaminationName(input.LaminationName, cancellationToken) &&
                TryValidationPos(input.Creasing) &&
                TryValidationPos(input.Drilling);
        }

        private bool TryValidationSize(int size)
        {
            return size > 0 && size < _printingMachineSetting.MaximumSizeLength;
        }

        private async Task<bool> TryValidationNamePaperAsync(string namePaper, CancellationToken cancellationToken)
        {
            try
            {
                return await _applicationContext.PaperCatalogs
                    .AsNoTracking()
                    .AnyAsync(paperCatalogs => paperCatalogs.Name == namePaper, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }

        private async Task<bool> TryValidationLaminationName(string? nameLamination, CancellationToken cancellationToken)
        {
            if (nameLamination != null && nameLamination != string.Empty)
            {
                try
                {
                    return await _applicationContext.Laminations
                        .AsNoTracking()
                        .AnyAsync(laminations => laminations.Name == nameLamination, cancellationToken);
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
