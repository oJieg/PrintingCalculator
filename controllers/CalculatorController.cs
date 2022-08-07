using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.controllers
{
    public class CalculatorController : Controller
    {
        private readonly ApplicationContext _BD;
        private readonly ILogger<CalculatorController> _logger;
        public CalculatorController(ApplicationContext DB, ILogger<CalculatorController> logger)
        {
            _logger = logger;
            _BD = DB;
        }

        public async Task<ActionResult> Index(int HistoryId)
        {
            PaperAndHistoryInput PaperAndHistoryInput = new();
            try
            {
                 PaperAndHistoryInput.Paper = await _BD.PaperCatalogs.ToListAsync();
                PaperAndHistoryInput.Lamination = await _BD.Laminations.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("не вышло получить из базы PaperCatalogs и laminations, {ex}", ex);
            }

            if (HistoryId != 0)
            {
                try
                {

                    PaperAndHistoryInput.Input = (await _BD.Historys.Where(s => s.Id == HistoryId).Include(x => x.Input).FirstAsync()).Input;
                }
                catch (Exception ex)
                {
                    _logger.LogError("error add DataBase historyID = {HistoryId}, {ex}", HistoryId, ex);
                }
            }

            return View("Calculator", PaperAndHistoryInput);
        }
    }
}