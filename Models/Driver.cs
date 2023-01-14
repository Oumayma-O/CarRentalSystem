using System.ComponentModel.DataAnnotations; 

namespace CarRentalSystem.Models
{
	public class Driver
	{
		public int Id { get; set; }
		[Required] public string? DriverName { get; set; }

        [Required] public string Address { get; set; }

        [Required] public string MobileNo { get; set; }

        [Required] public int Age { get; set; }

        [Required]
		public int Experience { get; set; }

        public string? ImagePath { get; set; }

        [Required] public IFormFile DriverImage { get; set; }





    }
}
