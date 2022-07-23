using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.controllers
{
    public class CalculatorController : Controller
    {
        private readonly ApplicationContext _BD;
        private readonly ILogger<HomesController> _logger;
        public CalculatorController(ApplicationContext DB, ILogger<HomesController> logger)
        {
            _logger = logger;
            _BD = DB;
        }

        public ActionResult Index(int HistoryId)
        {
            PaperAndHistoryInput paperAndInput = new();
            try
            {
                paperAndInput.Paper = _BD.PaperCatalogs.ToList();
                paperAndInput.Lamination = _BD.Laminations.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("не вышло получить из базы PaperCatalogs и laminations, {ex}", ex);
            }

            if (HistoryId != 0)
            {
                try
                {
                    paperAndInput.Input = _BD.Historys.Where(s => s.Id == HistoryId).Include(x => x.Input).First().Input;
                }
                catch (Exception ex)
                {
                    _logger.LogError("error add DataBase historyID = {HistoryId}, {ex}", HistoryId, ex);
                }
            }

            return View("Calculator", paperAndInput);
        }
    }
}