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

        public int GetMarkup(int sheets)
        {
            if (sheets >= _markup[^1].Page)
            {
                return _markup[^1].Markup;
            }
            MaxMin(sheets);

            float factor = (float)1 - ((float)(sheets - _sheets) / (float)_difference);
            return (int)(_stepMarkup * factor + _maxMarkup);
        }

        private void MaxMin(int shets)
        {
            for (int i = 0; i <= _markup.Count - 2; i++)
            {
                if (shets >= _markup[i].Page && shets < _markup[i + 1].Page)
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