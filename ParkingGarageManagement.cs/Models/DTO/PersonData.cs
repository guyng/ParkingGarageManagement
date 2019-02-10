using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class PersonData
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		[Required(ErrorMessage = "PersonId is required")]
		public string PersonId { get; set; }
		[Required(ErrorMessage = "Phone is required")]
		public string Phone { get; set; }
	}
}
