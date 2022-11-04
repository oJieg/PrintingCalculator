using Microsoft.AspNetCore.Mvc;

namespace printing_calculator.controllers
{
    public class MailTransferController : Controller
    {
        public IActionResult Admin()
        {
            return View("Admin");
        }
        public IActionResult Client()
        {
            return View("Client");
        }
    }
}
