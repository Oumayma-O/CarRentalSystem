using CarRentalSystem.Models;
using CarRentalSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
	public class RentController : Controller
	{
        private readonly IData data;
        public RentController(IData data)
        {
            this.data = data;
        }

        public IActionResult Index()
		{
			return View();
		}

        public IActionResult Add()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Add(Rent rent)
        {
            if (!ModelState.IsValid)
            {
                return View(rent);
            }
            bool isSaved = data.BookingNow(rent);
            ViewBag.isSaved = isSaved;
            ModelState.Clear();
            return View();
        }
        //[HttpPost]
        //public IActionResult GetBrand()
        //{
        //    return Json();
        //}


    }
}
