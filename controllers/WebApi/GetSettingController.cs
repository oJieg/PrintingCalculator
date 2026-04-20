//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using printing_calculator.DataBase;
//using printing_calculator.DataBase.setting;
//using printing_calculator.Models;

//namespace printing_calculator.controllers.WebApi
//{
//    [ApiController]
//    public class GetSettingController
//    {
//        private readonly ApplicationContext _applicationContext;

//        public GetSettingController(ApplicationContext applicationContext)
//        {
//            _applicationContext = applicationContext;
//        }

//        [HttpGet("GetJson")]
//        public async Task<SettingCalculation> GetSettingJson()
//        {
//            CommonToAllMarkup[] commonToAllMarkups = await _applicationContext.CommonToAllMarkups.ToArrayAsync();
//            Lamination[] laminations = await _applicationContext.Laminations.ToArrayAsync();
//            PaperCatalog[] paperCatalog = await _applicationContext.PaperCatalogs.Include(x=>x.Size).ToArrayAsync();
//            SizePaper[] paperSizes = await _applicationContext.SizePapers.ToArrayAsync();
//            PosMachinesSetting[] posMachines = await _applicationContext.PosMachinesSettings.Include(x=>x.Markups).ToArrayAsync();
//            PrintingMachineSetting printingsMachines = await _applicationContext.PrintingMachinesSettings.Include(x=>x.Markups).FirstAsync();
//            SpringBrochureSetting[] springBrochureSetting = await _applicationContext.SpringBrochureSettings.Include(x => x.SpringPrice).ToArrayAsync();

//            return new SettingCalculation()
//            {
//                CommonToAllMarkups = commonToAllMarkups,
//                Laminations = laminations,
//                PaperCatalog = paperCatalog,
//                PaperSizes = paperSizes,
//                PosMachines = posMachines,
//                PrintingsMachine = printingsMachines,
//                SpringBrochureSettings = springBrochureSetting
//            };
//        }
//    }
//}
