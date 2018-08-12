using Microsoft.AspNetCore.Mvc;

namespace DigitRecognizer.Controllers {
    public class PingController : Controller {
        [HttpGet("/ping")]
        public string Ping() {
            return "pong";
        }
    }
}