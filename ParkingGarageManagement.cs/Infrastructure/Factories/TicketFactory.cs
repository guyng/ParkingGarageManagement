using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Infrastructure.Enums;
using ParkingGarageManagement.cs.Models.Tickets;

namespace ParkingGarageManagement.cs.Infrastructure.Factories
{
	public class TicketFactory
	{
		public static ITicket Create(TicketType ticketType)
		{
			switch (ticketType)
			{
				case TicketType.Regular:
					{
						return new RegularTicket();
					}
				case TicketType.Value:
					{
						return new ValueTicket();
					}
				case TicketType.Vip:
					{
						return new VIPTicket();
					}
			}

			return null;
		}
	}
}
