using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Enums;

namespace ParkingGarageManagement.cs.Models.DTO
{
	public class CheckOutData
	{
		public int PersonId { get; set; }
		public VehicleType VehicleType { get; set; }
		public DateTime CheckOut { get; set; }
	}
}
