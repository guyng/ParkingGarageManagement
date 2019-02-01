using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class TicketClass
	{
		public int Id { get; set; }
		public int TicketId { get; set; }
		public int ClassId { get; set; }
	}
}
