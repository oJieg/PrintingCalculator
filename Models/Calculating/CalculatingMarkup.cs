using printing_calculator.DataBase;
using Microsoft.Extensions.Options;
using printing_calculator.Models.markup;

namespace printing_calculator.Models
{
    public class CalculatingMarkup // переделать под хранения markup в json
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
            for(int i = 0; i < _markup.Count-2; i++)
            {
                if (Shets >= _markup[i].Page || Shets < _markup[i+1].Page)
                {
                    difference = _markup[i + 1].Page - _markup[i].Page;
                    stepMarkup = _markup[i].Markup-_markup[i+1].Markup;
                    _maxMarkup = _markup[i + 1].Markup;
                    _sheets = _markup[i].Page;
                    return;
                }
            }

            //последний
        }

        //private void MaxMin(int Sheets)
        //{
        //    if (Sheets < 15)
        //    {
        //        difference = 15;
        //        stepMarkup = _markup.Markup0 - _markup.Markup15;
        //        _maxMarkup = _markup.Markup15;
        //        _sheets = 1;
        //    }
        //    else if (Sheets < 30)
        //    {
        //        difference = 15;
        //        stepMarkup = _markup.Markup15 - _markup.Markup30;
        //        _maxMarkup = _markup.Markup30;
        //        _sheets = 15;
        //    }
        //    else if (Sheets < 60)
        //    {
        //        difference = 30;
        //        stepMarkup = _markup.Markup30 - _markup.Markup60;
        //        _maxMarkup = _markup.Markup60;
        //        _sheets = 30;
        //    }
        //    else if (Sheets < 120)
        //    {
        //        difference = 60;
        //        stepMarkup = _markup.Markup60 - _markup.Markup120;
        //        _maxMarkup = _markup.Markup120;
        //        _sheets = 60;
        //    }
        //    else if (Sheets < 250)
        //    {
        //        difference = 130;
        //        stepMarkup = _markup.Markup120 - _markup.Markup250;
        //        _maxMarkup = _markup.Markup250;
        //        _sheets = 120;
        //    }
        //}
    }
}
