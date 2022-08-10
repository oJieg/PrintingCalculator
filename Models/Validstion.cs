using printing_calculator.ViewModels;
using printing_calculator.DataBase;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.Models
{
    public class Validstion
    {
        private readonly ApplicationContext _BD;
        public Validstion(ApplicationContext DB)
        {
            _BD = DB;
        }

        public async Task<bool> TryValidationInpytAsync(Input input)
        {
            return input != null &
                TryValidationSize(input.Whidth) &
                TryValidationSize(input.Height) &
                await TryValidationNamePaperAsync(input.Paper) &
                TryPositiveNumber(input.Amount) &
                TryPositiveNumber(input.Kinds) &
                //  input.Duplex != null &
                await TryValidationLaminationName(input.LaminationName) &
                TryValidationPos(input.Creasing) &
                TryValidationPos(input.Drilling);
        }

        private static bool TryValidationSize(int? size)
        {
            return size != null & size > 0 & size > 0;
        }

        private async Task<bool> TryValidationNamePaperAsync(string namePaper)
        {
            return await _BD.PaperCatalogs.AnyAsync(x => x.Name == namePaper);
        }

        private async Task<bool> TryValidationLaminationName(string nameLamination)
        {
            return await _BD.Laminations.AnyAsync(x => x.Name == nameLamination);
        }

        private bool TryPositiveNumber(int number)
        {
            return number > 0 & number < int.MaxValue;
        }
        private bool TryValidationPos(int countPos)
        {
            if (countPos == null)
                return true;
            return countPos > 0 & countPos < 50; //не больше 50 биговок и дырок)) 
        }
    }
}
