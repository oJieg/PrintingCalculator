using printing_calculator.DataBase;
namespace printing_calculator.Models
{
    public class SplittingPaper
    {
        private readonly int _whiteFieldWidth = 5; //вынести в json
        private readonly int _whiteFieldHeight = 6; //вынести в json
        private readonly int _fieldForLabels = 4; //вынести в json
        private readonly float _bleed = 3; //вынести в json
        public SplittingPaper()
        {

        }
        public int Splitting(SizePaper sizePaper, float SizeProdyctionHeight, float SizeProdyctionWidth)
        {
            int SizePaperHeight = sizePaper.SizePaperHeight - _whiteFieldHeight;
            int SizePaperWidth = sizePaper.SizePaperWidth - _whiteFieldWidth;

            int HorizontOrientation = PiecesPerSheet(SizePaperHeight, SizePaperWidth, SizeProdyctionHeight, SizeProdyctionWidth);
            int VerticalOrientation = PiecesPerSheet(SizePaperWidth, SizePaperHeight, SizeProdyctionHeight, SizeProdyctionWidth);

            if(HorizontOrientation>VerticalOrientation)
            {
                return HorizontOrientation;
            }
            else
            {
                return VerticalOrientation;
            }
        }
        private int PiecesPerSheet(int sizePaperHorizontal, int sizePaperHVertical, float sizeProdyctionHeight, float sizeProdyctionWidth)
        {
            sizeProdyctionHeight = sizeProdyctionHeight + _bleed;
            sizeProdyctionWidth = sizeProdyctionWidth + _bleed;

            sizePaperHorizontal = sizePaperHorizontal - _fieldForLabels;
            sizePaperHVertical = sizePaperHVertical - _fieldForLabels;

            int horizontal = (int)((float)sizePaperHorizontal / sizeProdyctionHeight);
            int vertical = (int)((float)sizePaperHVertical / sizeProdyctionWidth);

            return horizontal * vertical;
        }
    }
}
