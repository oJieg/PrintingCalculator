using Microsoft.AspNetCore.Mvc;
using printing_calculator.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace printing_calculator.controllers
{
    public class CalculatorController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly ILogger<CalculatorController> _logger;
        public CalculatorController(ApplicationContext applicationContext, ILogger<CalculatorController> logger)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        public async Task<ActionResult> Index(int historyId, CancellationToken cancellationToken)
        {
            PaperAndHistoryInput PaperAndHistoryInput = new();
            try
            {
                PaperAndHistoryInput.Paper = await _applicationContext.PaperCatalogs
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToListAsync(cancellationToken);
                PaperAndHistoryInput.Lamination = await _applicationContext.Laminations
                    .ToListAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return new EmptyResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "не вышло получить из базы PaperCatalogs " +
                    "и laminations");
            }

            if (historyId != 0)
            {
                try
                {
                    PaperAndHistoryInput.Input = (await _applicationContext.Histories
                        .Where(historys => historys.Id == historyId)
                        .Include(historys => historys.Input)
                        .FirstAsync(cancellationToken))
                        .Input;
                }
                catch (OperationCanceledException)
                {
                    return new EmptyResult();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error add DataBase historyID = {HistoryId}", historyId);
                }
            }

            return View("Calculator", PaperAndHistoryInput);
        }
    }
}