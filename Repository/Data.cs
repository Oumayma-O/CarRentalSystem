using CarRentalSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting; 
using System.Data.OleDb;
using System.Runtime.Versioning;


namespace CarRentalSystem.Repository
{
	public class Data : IData
	{
        private readonly IConfiguration configuration;
        private readonly string dbcon = "";
        private readonly IWebHostEnvironment webhost; 
    

        public Data(IConfiguration configuration , IWebHostEnvironment webhost)
        {
            this.configuration = configuration;
            dbcon = this.configuration.GetConnectionString("dbConnection");
            this.webhost= webhost; 
            
        }
        [SupportedOSPlatform("windows")]

        public bool BookingNow(Rent rent)
        {
            bool isSaved = false;
            OleDbConnection con = GetOLeConnection();
            try
            {
                con.Open();
                rent.TotalAmount = rent.TotalRun + rent.Rate; 
                string qry = String.Format("Insert into Rents (PickUp , DropOff, PickUpDate,DropOffDate , TotalRun , Rate, TotalAmount,Brand, Model, DriverId , CustomerName, CustomerContact ) values (" +
                    "'{0},'{1}','{2}','{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')", rent.PickUp, rent.DropOff, rent.PickUpDate, rent.DropOffDate , rent.TotalRun, rent.Rate, rent.TotalAmount, 
                    rent.Brand, rent.Model, rent.DriverId , rent.CustomerName , rent.CustomerContact);
                isSaved = SaveData(qry, con);
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return isSaved; 
        }
    public List<Driver> GetAllDrivers()
        {


            List<Driver> drivers = new List<Driver>();

            OleDbConnection con = new OleDbConnection(dbcon);
                try
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM Driver", con);
                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                    Driver driver = new Driver();
                    driver.DriverName = reader["DriverName"].ToString();
                    driver.Address = reader["Address"].ToString();
                    driver.MobileNo = reader["MobileNo"].ToString();
                    driver.Age = Convert.ToInt32(reader["Age"]);
                    driver.Experience = Convert.ToInt32(reader["Experience"]);
                    driver.ImagePath = reader["ImagePath"].ToString();
                    drivers.Add(driver);


                }
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
                finally
                {
                    con.Close();
                }
            return drivers;
        }
            


              

        public bool AddDriver(Driver newdriver)
        {
            bool isSaved = false;
            OleDbConnection con = GetOLeConnection();

            try
            {
                con.Open();
                newdriver.ImagePath = SaveImage(newdriver.DriverImage, "drivers");
                string qry = String.Format("Insert into Drivers(DriverName , Address , MobileNo , Age, Experience ,  ImagePath) values('{0}' , '{1}' , '{2}' , {3} , {4} , '{5}')",
                    newdriver.DriverName, newdriver.Address, newdriver.MobileNo, newdriver.Age, newdriver.Experience, newdriver.ImagePath);
                isSaved = SaveData(qry, con); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return isSaved;
        }

        
        public List<Car> GetAllCars()
        {


            List<Car> cars = new List<Car>();
        
            OleDbConnection con = GetOLeConnection(); 
           try{
                con.Open();
                string query = "select * from Car";
                OleDbDataReader reader = GetData(query, con);
                while (reader.Read())
                {
                    Car car = new Car();
                    car.Id = Convert.ToInt32(reader["Id"]);
                    car.Brand = reader["Brand"].ToString();
                    car.Model = reader["Model"].ToString();
                    car.PassingYear = Convert.ToInt32(reader["PassingYear"]);
                    car.CarNumber = reader["CarNumber"].ToString();
                    car.Engine = reader["Engine"].ToString();
                    car.FuelType = reader["FuelType"].ToString();
                    car.SeatingCapacity = Convert.ToInt32(reader["SeatingCapacity"]);
                    car.ImagePath = reader["ImagePath"].ToString();
                    cars.Add(car);
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cars;
        }
        

        private OleDbDataReader GetData(string query ,OleDbConnection con)
        {
            OleDbDataReader reader = null;
            try
            {

                OleDbCommand cmd = new OleDbCommand(query, con);
                reader = cmd.ExecuteReader();
            }
            catch (Exception)
            {
                return null;
            }
            return reader;
        }
       

        public bool AddNewCar(Car newcar)
        {
            bool isSaved = false;
            OleDbConnection con = GetOLeConnection(); 

            try
            {
                con.Open();
                newcar.ImagePath = SaveImage(newcar.CarImage, "cars"); 
                string qry = String.Format("Insert into Cars(Brand , Model , PassingYear , CarNumber, Engine , FuelType, ImagePath ,SeatingCapacity ) values +\r\n                    (\" '{0}','{1}','{2}','{3}','{4}','{5}', '{6}' , {7}\")",
                    newcar.Brand ,newcar.Model, newcar.PassingYear , newcar.CarNumber , newcar.Engine , newcar.FuelType ,newcar.ImagePath ,newcar.SeatingCapacity  ) ;
                isSaved = SaveData(qry, con); 
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            return isSaved;
        }

        private string SaveImage(IFormFile file , string folderName)
        {
            string imagepath = "";
            try
            {
                string uploadfolder = Path.Combine(webhost.WebRootPath, "images/"+folderName);
                imagepath = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filepath = Path.Combine(uploadfolder, imagepath); 
                using(FileStream filestream = new FileStream(filepath, FileMode.Create))
                {
                    file.CopyTo(filestream); 
                }

            }
            catch (Exception)
            {
                throw;
            }
            return imagepath; 
        }
        [SupportedOSPlatform("windows")] // adding because ms access support in windows only 
        
        private OleDbConnection GetOLeConnection()
        {
            return new OleDbConnection(dbcon);
        }

        private bool SaveData(string qry , OleDbConnection con)
        {
            bool isSaved = false;
            try
            {
                OleDbCommand command = new OleDbCommand(qry, con);
                command.ExecuteNonQuery();

                isSaved = true;

            }
            catch (Exception)
            {
                throw; 
            }

            return isSaved; 
        }

        
    }
}
