using Microsoft.AspNetCore.Mvc;

namespace printing_calculator.controllers
{
    public class OrderListController : Controller
    {
        public IActionResult Index()
        {
            return View("OrderList");
        }
    }
}
