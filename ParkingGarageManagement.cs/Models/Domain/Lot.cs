using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class Lot
	{
		public int Id { get; set; }
		public int LotPosition { get; set; }
		public int VehicleId { get; set; }
		public virtual Vehicle Vehicle { get; set; }
		public DateTime CheckIn { get; set; }
	}
}
