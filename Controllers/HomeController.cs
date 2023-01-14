using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CarRentalSystem.Repository; 

namespace CarRentalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IData data;

        
        public HomeController(ILogger<HomeController> logger , IData data)
        {
            _logger = logger;
            this.data = data; 
        }
        public IActionResult Index()
        {
            var list = data.GetAllCars();
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}