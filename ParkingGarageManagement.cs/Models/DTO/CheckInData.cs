using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Enums;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class CheckInData
	{
		public VehicleDimensionData VehicleDimensionData { get; set; }
		public PersonData PersonData { get; set; }

		[Required(ErrorMessage = "VehicleType is required")]
		public VehicleType VehicleType { get; set; }
		[Required(ErrorMessage = "TicketType is required")]
		public TicketType TicketType { get; set; }
	}
}
