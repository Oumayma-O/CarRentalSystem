using CarRentalSystem.Models;
using CarRentalSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
	public class DriverController : Controller
	{
        private readonly IData data;
        public DriverController(IData data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {
            var list = data.GetAllDrivers();
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Driver newdriver)
        {
            if (!ModelState.IsValid)
            {
                return View(newdriver);
            }
            bool isSaved = data.AddDriver(newdriver);
            ViewBag.isSaved = isSaved;
            ModelState.Clear();
            return View();
        }
    }
}
