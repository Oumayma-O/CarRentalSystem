using CarRentalSystem.Models;
namespace CarRentalSystem.Repository
{
	public interface IData
	{
		public bool AddNewCar(Car newcar);
        public List<Car> GetAllCars();

        public bool AddDriver(Driver newdriver);
        public List<Driver> GetAllDrivers();

        public bool BookingNow(Rent rent);
        //List<string> GetBrand(); 


    }
}
