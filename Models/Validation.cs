using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class Validation
    {
        private readonly ApplicationContext _applicationContext;
        public Validation(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
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

        private static bool TryValidationSize(int? size)
        {
            return size != null && size > 0 && size < 2000;
        }

        private async Task<bool> TryValidationNamePaperAsync(string namePaper, CancellationToken cancellationToken)
        {
            return await _applicationContext.PaperCatalogs
                .AsNoTracking()
                .AnyAsync(x => x.Name == namePaper, cancellationToken);
        }

        private async Task<bool> TryValidationLaminationName(string nameLamination, CancellationToken cancellationToken)
        {
            if (nameLamination != "none")
            {
                try
                {
                    return await _applicationContext.Laminations
                        .AsNoTracking()
                        .AnyAsync(x => x.Name == nameLamination, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return false;
                }
            }
            return true;
        }

        private bool TryPositiveNumber(int number)
        {
            return number > 0;
        }
        private bool TryValidationPos(int countPos)
        {
            if (countPos == 0)
                return true;
            return countPos > 0 && countPos < 50; //не больше 50 биговок и дырок)) 
        }
    }
}
