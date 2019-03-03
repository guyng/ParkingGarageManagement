using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;
using TicketType = ParkingGarageManagement.cs.Infrastructure.Enums.TicketType;

namespace ParkingGarageManagement.cs.Validators
{
	public class VehicleDimensionValidator
	{
		public static bool Validate(VehicleDimensionData vehicleDimensionData,Ticket ticket)
		{
			return ticket.MaxHeight >= vehicleDimensionData.Height && ticket.MaxWidth >= vehicleDimensionData.Width
			                                                       && ticket.MaxLength >= vehicleDimensionData.Length ||
																   (ticket.TicketType == TicketType.Vip);
		}
	}
}
