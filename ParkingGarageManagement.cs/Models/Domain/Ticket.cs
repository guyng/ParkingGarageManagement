using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class Ticket
	{
		public int Id { get; set; }
		public int TicketTypeId { get; set; }
		public virtual TicketType TicketType { get; set; }
		public string Name { get; set; }
		public int MaxHeight { get; set; }
		public int MaxWidth { get; set; }
		public int MaxLength { get; set; }
		public int TimeLimit { get; set; }
		public int Cost { get; set; }
	}
}
