using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace printing_calculator.Models
{
    public class Validation
    {
        private readonly ApplicationContext _applicationContext;
        private readonly Setting _settings;

        public Validation(ApplicationContext applicationContext, IOptions<Setting> options)
        {
            _applicationContext = applicationContext;
            _settings = options.Value;
        }

        public async Task<bool> TryValidateInputAsync(Input input, CancellationToken cancellationToken)
        {
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
            return size > 0 && size < _settings.SettingPrinter.MaximumSize;
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
            if (nameLamination != null)
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
            return countPos == 0 || (countPos > 0 && countPos < _settings.Pos.MaximumAmount);
        }
    }
}
