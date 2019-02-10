using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class LotRange
	{
		public int Id { get; set; }
		public int TicketId { get; set; }
		public virtual Ticket Ticket { get; set; }
		public int MinRange { get; set; }
		public int MaxRange { get; set; }
	}
}
