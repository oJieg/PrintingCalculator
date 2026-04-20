using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printing_calculator.controllers.WebApi.RequestModels;
using printing_calculator.Models;
using printing_calculator.Singletones.Interfases;
using printing_calculator.ViewModels;

namespace printing_calculator.controllers
{
    public class CalculatorController : Controller
    {
        private readonly ISettingStore _settingStore;
        private readonly ILogger<CalculatorController> _logger;
        public CalculatorController(ISettingStore settingStore, ILogger<CalculatorController> logger)
        {
            _logger = logger;
            _settingStore = settingStore;
        }

        public async Task<ActionResult> Index(int historyId, int productId, int orderId, CancellationToken cancellationToken)
        {
            SettingsInfoForCalculation PaperAndHistoryInput = new();
            try
            {
                ISettingCalculation setting = await _settingStore.GetSettings();

                PaperAndHistoryInput.Paper = setting.PaperCatalog
                    .Where(paper => paper.Status > 0)
                    .OrderBy(paper => paper.Id)
                    .ToList();
                PaperAndHistoryInput.Lamination =  setting.Laminations
                    .Where(lamination => lamination.Status > 0)
                    .OrderBy(lamunation => lamunation.Id)
                    .ToList();
                PaperAndHistoryInput.commonToAllMarkups = setting.CommonToAllMarkups
                    .ToList();

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

            //if (historyId != 0)
            //{
            //    try
            //    {
            //        PaperAndHistoryInput.Input = (await _settingStore.Histories
            //            .Where(historys => historys.Id == historyId)
            //            .Include(historys => historys.Input)
            //            .FirstAsync(cancellationToken))
            //            .Input;
            //    }
            //    catch (OperationCanceledException)
            //    {
            //        return new EmptyResult();
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "error add DataBase historyID = {HistoryId}", historyId);
            //    }
            //}

            return View("Calculator", PaperAndHistoryInput);
        }
    }
}