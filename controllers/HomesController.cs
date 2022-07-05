using Microsoft.AspNetCore.Mvc;
using printing_calculator.DataBase;

namespace printing_calculator.controllers
{
    public class HomesController : Controller
    {
        private ApplicationContext DB;
        public HomesController(ApplicationContext context)
        {
            DB = context;
        }
        public IActionResult Index()
        {
            //DB.PaperCatalogs.Add(new PaperCatalog { Name = "mondi 300", Price = (float)15.2 });
            //DB.PaperCatalogs.Add(new PaperCatalog { Name = "xerox 130", Price = (float)5.8 });
            //DB.PaperCatalogs.Add(new PaperCatalog { Name = "sholk 350", Price = (float)18.7 });
            //DB.SaveChanges();
            //DB.TestTables.Add(new TestTable { test = 1 });
            //DB.SaveChanges();

            //var user = DB.TestTables.ToList();
            //string otv = "";
            //foreach (TestTable u in user)
            //{
            //    otv += $"{u.test} - ";
            //}
            //return new ObjectResult(otv);
            return View("Page");
        }
    }
}
