using Microsoft.AspNetCore.Mvc;
using ReportesMVC.Models;
using ReportesMVC.Services;
using System.Diagnostics;

namespace ReportesMVC.Controllers
{
	[ValidarSesionAttributeService]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult CerrarSesion()
		{
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Ingreso");
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}