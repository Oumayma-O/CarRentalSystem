using CarRentalSystem.Models;
using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Repository; 

namespace CarRentalSystem.Controllers
{
    public class CarController : Controller
    {
        private readonly IData data;
        public CarController(IData data)
        {
            this.data = data;
        }
        
        public IActionResult Index()
        {
            var list = data.GetAllCars();
            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Car newCar)
        {
            if (!ModelState.IsValid)
            {
                return View(newCar);
            }
            bool isSaved = data.AddNewCar(newCar);
            ViewBag.isSaved = isSaved; 
            ModelState.Clear();
            return View();
        }
    }
}
