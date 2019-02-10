using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class TicketType
	{
		public int Id { get; set; }
		public Infrastructure.Enums.TicketType Type { get; set; }
	}
}
