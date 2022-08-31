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

        public async Task<bool> TryValidationInpytAsync(Input input, CancellationToken cancellationToken)
        {
            return input != null &
                TryValidationSize(input.Whidth) &&
                TryValidationSize(input.Height) &&
                await TryValidationNamePaperAsync(input.Paper, cancellationToken) &&
                TryPositiveNumber(input.Amount) &&
                TryPositiveNumber(input.Kinds) &&
                await TryValidationLaminationName(input.LaminationName, cancellationToken) &&
                TryValidationPos(input.Creasing) &&
                TryValidationPos(input.Drilling);
        }

        private  bool TryValidationSize(int? size)
        {
            return size != null && size > 0 && size < _settings.SettingPrinter.MaximumSize;
        }

        private async Task<bool> TryValidationNamePaperAsync(string namePaper, CancellationToken cancellationToken)
        {
            return await _applicationContext.PaperCatalogs
                .AsNoTracking()
                .AnyAsync(paperCatalogs => paperCatalogs.Name == namePaper, cancellationToken);
        }

        private async Task<bool> TryValidationLaminationName(string nameLamination, CancellationToken cancellationToken)
        {
            if (nameLamination != Constants.ReturnEmptyOutputHttp)
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

        private static bool TryPositiveNumber(int number)
        {
            return number > 0;
        }

        private bool TryValidationPos(int countPos)
        {
            if (countPos == 0)
                return true;
            return countPos > 0 && countPos < _settings.Pos.MaximumAmount; //не больше 50 биговок и дырок)) 
        }
    }
}
