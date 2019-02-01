using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class VehicleType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ClassId { get; set; }
		public Class Class { get; set; }
	}
}
