using Microsoft.AspNetCore.Mvc;

namespace printing_calculator.controllers
{
    public class OrderController : Controller
    {
        public IActionResult List()
        {
            return View("OrderList");
        }

        public IActionResult Detal(int id) {
			return View("DetalOrder", id);
		}
    }
}
