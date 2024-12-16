// Controllers/HomeController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeihnachtsTannenbaum.Services;

namespace WeihnachtsTannenbaum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TasmotaService _tasmotaService;

        public HomeController(ILogger<HomeController> logger, TasmotaService tasmotaService)
        {
            _logger = logger;
            _tasmotaService = tasmotaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Toggle()
        {
            try
            {
                await _tasmotaService.TogglePowerAsync();
                TempData["Message"] = "Tannenbaum umgeschaltet!";
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Umschalten des Tannenbaums.");
                TempData["Message"] = "Fehler beim Umschalten des Tannenbaums.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Optional: Error-Handling-Action
    }
}