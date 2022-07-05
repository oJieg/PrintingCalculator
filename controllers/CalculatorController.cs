using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;
using printing_calculator.Models;
using System.Web;
//using printing_calculator.Views.calculator.CalculatorModel;

namespace printing_calculator.controllers
{
    public class CalculatorController : Controller
    {
        private ApplicationContext _BD;
        public CalculatorController(ApplicationContext DB)
        {
            _BD = DB;
        }
        
        public ActionResult Index(int HistoryId)
        {
            PaperAndHistoryInput paperAndInput = new() 
            { 
                Paper = _BD.PaperCatalogs.ToList() 
            };
            if(HistoryId!=0)
            {
                paperAndInput.Input = _BD.Historys.Where(s => s.Id == HistoryId).Include(x => x.Input).First().Input;
            }

            return View("Calculator", paperAndInput);
        }
    }
}
