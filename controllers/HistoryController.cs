using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using printing_calculator.Models;
using printing_calculator.DataBase;
using Microsoft.Extensions.Options;

namespace printing_calculator.controllers
{
    public class HistoryController : Controller
    {
        private ApplicationContext _BD;
        private IOptions<Setting> _options;
        public HistoryController(ApplicationContext context, IOptions<Setting> options)
        {
            _BD = context;
            _options = options;
        }

        public IActionResult Index(int page, int countPage = 10)
        {
            List<SimplResult> result = new();
            FullIncludeHistory fullIncludeHistory = new();
            List<History> histories = fullIncludeHistory.GetList(_BD, page, countPage);

            foreach (History history in histories)  //наименование поменяй на человеческие!!!!
            {
              
                result.Add(HistoryToSimplResult(history));
            }

            return View("History", result);
        }
        public SimplResult HistoryToSimplResult(History history)
		{
            SimplResult result = new SimplResult();
            result.HistoryId = history.Id;
            result.Whidth = history.Input.Whidth;
            result.Height = history.Input.Height;
            result.Amount = history.Input.Amount;
            result.Kinds = history.Input.Kinds;
            result.PaperName = history.Input.Paper.Name;
            if(history.Input.Lamination == null)
			{
                result.Lamination = false;
            }
            else
			{
                result.Lamination = true;
			}

            if(history.CreasingPrice >0)
			{
                result.Creasing = true;
			}
            else
			{
                result.Creasing=false;
			}

            if(history.DrillingPrice>0)
			{
                result.Drilling = true;
			}
			else
			{
                result.Drilling=false;
			}

            if(history.RoundingPrice >0)
			{
                result.Drilling=true;
			}
			else
			{
                result.Drilling = false;
			}

            result.Price =(int)history.Price;
            return result;
		}
    }
}
