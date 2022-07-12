using printing_calculator.DataBase;
using Microsoft.Extensions.Options;
using printing_calculator.Models.Settings;

namespace printing_calculator.Models
{
    public class CalculatingMarkup
    {
        private float stepMarkup;
        private float _maxMarkup;


        private int difference;
        private int _sheets;
        private List<MarkupList> _markup;

        public CalculatingMarkup(List<MarkupList> list)
        {
            _markup = list;
        }
        public int GetMarkup(int Sheets)
        {
            if (Sheets >=_markup[_markup.Count-1].Page)
            {
                return _markup[_markup.Count - 1].Page;
            }
            MaxMin(Sheets);

            float factor = 1 - ((Sheets - _sheets) / difference);
            return (int)(stepMarkup * factor + _maxMarkup);
        }

        private void MaxMin(int Shets)
        {
            for(int i = 0; i <= _markup.Count-2; i++)
            {
                if (Shets >= _markup[i].Page && Shets < _markup[i+1].Page)
                {
                    difference = _markup[i + 1].Page - _markup[i].Page;
                    stepMarkup = _markup[i].Markup-_markup[i+1].Markup;
                    _maxMarkup = _markup[i + 1].Markup;
                    _sheets = _markup[i].Page;
                    return;
                }
            }
        }
    }
}
