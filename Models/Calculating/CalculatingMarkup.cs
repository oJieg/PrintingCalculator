using printing_calculator.Models.Settings;
using printing_calculator.DataBase.setting;

namespace printing_calculator.Models
{
    public class CalculatingMarkup
    {
        private float _stepMarkup;
        private float _maxMarkupInBorder;
        private int _differenceMarkup;
        private int _minimumBorderSheetsForMarkup;
        private readonly List<DataBase.setting.Markups> _markup;


        public CalculatingMarkup(List<DataBase.setting.Markups> markups)
        {
            _markup = markups;
        }

        public int GetMarkup(int sheets)
        {
            int returnMaxMarkup = _markup[^1].MarkupForThisPage;
            if (sheets >= _markup[^1].Page)
            {
                return returnMaxMarkup;
            }
  
            HittingBorder(sheets);
            if(_differenceMarkup == 0)
            {
                return returnMaxMarkup;
            }

            float factor = (float)1 - ((float)(sheets - _minimumBorderSheetsForMarkup) / (float)_differenceMarkup);
            return (int)(_stepMarkup * factor + _maxMarkupInBorder);
        }

        /// <summary>
        /// определяем в какие границы попадает количество листов и на этой основе заполняем приватные переменные
        /// </summary>
        /// <param name="shets"></param>
        private void HittingBorder(int shets) 
        {
            for (int i = 0; i <= _markup.Count - 2; i++)
            {
                if (shets >= _markup[i].Page && shets < _markup[i + 1].Page)
                {
                    _differenceMarkup = _markup[i + 1].Page - _markup[i].Page;
                    _stepMarkup = _markup[i].MarkupForThisPage - _markup[i + 1].MarkupForThisPage;
                    _maxMarkupInBorder = _markup[i + 1].MarkupForThisPage;
                    _minimumBorderSheetsForMarkup = _markup[i].Page;
                    return;
                }
            }
        }
    }
}