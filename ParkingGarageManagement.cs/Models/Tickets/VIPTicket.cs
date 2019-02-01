using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Constants;

namespace ParkingGarageManagement.cs.Models.Tickets
{
	public class VIPTicket : ITicket
	{
		public IEnumerable<int> LotsPositions { get; set; } = Enumerable.Range(1, 10);
		public int MaxHeight { get; set; } = TicketConstants.Unlimited;
		public int MaxWidth { get; set; } = TicketConstants.Unlimited;
		public int MaxLength { get; set; } = TicketConstants.Unlimited;
		public string Classes { get; set; } = TicketConstants.Unlimited.ToString();
		public int TimeLimit { get; set; } = TicketConstants.Unlimited;
		public int Cost { get; set; } = TicketConstants.Unlimited;
	}
}
