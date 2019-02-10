using System;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingGarageManagement.cs.Infrastructure.Enums;

namespace ParkingGarageManagement.cs.Models.Domain
{
	public class Vehicle
	{
		public int Id { get; set; }
		public int TicketId { get; set; }
		public virtual Ticket Ticket { get; set; }
		public int VehicleTypeId { get; set; }
		public virtual VehicleType VehicleType { get; set; }
		public int PersonId { get; set; }
		public int VehicleHeight { get; set; }
		public int VehicleWidth { get; set; }
		public int VehicleLength { get; set; }
	}
}
