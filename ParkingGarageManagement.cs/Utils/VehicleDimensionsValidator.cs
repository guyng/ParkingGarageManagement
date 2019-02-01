using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models.DTO;
using ParkingGarageManagement.cs.Models.Tickets;

namespace ParkingGarageManagement.cs.Utils
{
	public class VehicleDimensionsValidator
	{
		public static bool Validate(VehicleDimensionData vehicleDimensionData, ITicket ticket)
		{
			if (vehicleDimensionData == null || ticket == null)
				return false;

			return ticket.MaxHeight >= vehicleDimensionData.Height && ticket.MaxWidth >= vehicleDimensionData.Width
															   && ticket.MaxLength >= vehicleDimensionData.Length;
		}
	}
}
