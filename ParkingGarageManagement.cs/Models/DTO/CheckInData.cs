using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Enums;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class CheckInData
	{
		public VehicleDimensionData VehicleDimensionData { get; set; }
		public PersonData PersonData { get; set; }

		public VehicleType VehicleType { get; set; }
		public TicketType TicketType { get; set; }
	}
}
