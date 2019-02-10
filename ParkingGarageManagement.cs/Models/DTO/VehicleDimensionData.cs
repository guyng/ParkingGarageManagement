using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class VehicleDimensionData
	{
		[Required(ErrorMessage = "Height is required")]
		public int? Height { get; set; }
		[Required(ErrorMessage = "Width is required")]
		public int? Width { get; set; }
		[Required(ErrorMessage = "Length is required")]
		public int? Length { get; set; }
	}
}
