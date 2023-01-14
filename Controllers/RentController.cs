using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
	public class RentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Add()
        {
            return View();
        }
    }
}
