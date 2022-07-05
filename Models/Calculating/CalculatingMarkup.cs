using printing_calculator.DataBase;

namespace printing_calculator.Models
{
    public class CalculatingMarkup // переделать под хранения markup в json
    {
        private float _minMarkup;
        private float _maxMarkup;


        private float difference;
        private int _sheets;
        public float GetMarkup(Markup markup, int Sheets)
        {
            if (Sheets >= 250)
            {
                return markup.MarkupMuch;
            }
            MaxMin(markup, Sheets);

            float stepMarkup = _minMarkup - _maxMarkup;
            float factor = (difference - _sheets) / (float)10;
            return stepMarkup * factor + _maxMarkup;
        }

        private void MaxMin(Markup markup, int Sheets)
        {
            if (Sheets < 15)
            {
                difference = 15;
                _minMarkup = markup.Markup0;
                _maxMarkup = markup.Markup15;
                _sheets = Sheets;
            }
            else if (Sheets < 30)
            {
                difference = 15;
                _minMarkup = markup.Markup15;
                _maxMarkup = markup.Markup30;
                _sheets -= 15;
            }
            else if (Sheets < 60)
            {
                difference = 30;
                _minMarkup = markup.Markup30;
                _maxMarkup = markup.Markup60;
                _sheets -= 30;
            }
            else if (Sheets < 120)
            {
                difference = 60;
                _minMarkup = markup.Markup60;
                _maxMarkup = markup.Markup120;
                _sheets -= 60;
            }
            else if (Sheets < 250)
            {
                difference = 130;
                _minMarkup = markup.Markup120;
                _maxMarkup = markup.Markup250;
                _sheets -= 120;
            }
        }
    }
}
