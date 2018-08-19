using Microsoft.AspNetCore.Mvc;

namespace DigitRecognizer.Controllers {
    public class Home : Controller {
        [HttpGet("/")]
        [HttpGet("/test")]
        public IActionResult Index() {
            return View();
        }
    }
}