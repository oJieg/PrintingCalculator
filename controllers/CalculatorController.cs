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

        public async Task<ActionResult> Index(int historyId, int productId, int orderId, CancellationToken cancellationToken)
        {
            PaperAndHistoryInput PaperAndHistoryInput = new();
            try
            {
                PaperAndHistoryInput.Paper = await _applicationContext.PaperCatalogs
                    .Include(paper => paper.Size)
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToListAsync(cancellationToken);
                PaperAndHistoryInput.Lamination = await _applicationContext.Laminations
                    .Where(lamination => lamination.Status > 0)
                    .OrderBy(lamunation => lamunation.Id)
                    .ToListAsync(cancellationToken);
                PaperAndHistoryInput.commonToAllMarkups = await _applicationContext.CommonToAllMarkups
                    .ToListAsync();
                PaperAndHistoryInput.ProductId = productId;
                PaperAndHistoryInput.OpderId = orderId;
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