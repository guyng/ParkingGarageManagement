using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Enums;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class CheckOutData
	{
		public int VehicleId { get; set; }
		public DateTime Checkout { get; set; }
	}
}
