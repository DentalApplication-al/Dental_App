using Microsoft.AspNetCore.Mvc;

namespace DentalAPI.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("callback")]
        public IActionResult Callback(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Authorization code is missing");
            }

            // Here, you can handle the code, e.g., exchange it for an access token
            return Ok($"Authorization code: {code}");
        }
    }
}
