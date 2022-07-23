using printing_calculator.DataBase;
using Microsoft.Extensions.Options;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models
{
    public class CalculatingMarkup
    {
        private float _stepMarkup;
        private float _maxMarkup;


        private int _difference;
        private int _sheets;
        private readonly List<MarkupList> _markup;

        public CalculatingMarkup(List<MarkupList> list)
        {
            _markup = list;
        }

        public int GetMarkup(int Sheets)
        {
            if (Sheets >= _markup[^1].Page)
            {
                return _markup[^1].Markup;
            }
            MaxMin(Sheets);

            float factor = (float)1 - ((float)(Sheets - _sheets) / (float)_difference);
            return (int)(_stepMarkup * factor + _maxMarkup);
        }

        private void MaxMin(int Shets)
        {
            for (int i = 0; i <= _markup.Count - 2; i++)
            {
                if (Shets >= _markup[i].Page && Shets < _markup[i + 1].Page)
                {
                    _difference = _markup[i + 1].Page - _markup[i].Page;
                    _stepMarkup = _markup[i].Markup - _markup[i + 1].Markup;
                    _maxMarkup = _markup[i + 1].Markup;
                    _sheets = _markup[i].Page;
                    return;
                }
            }
        }
    }
}