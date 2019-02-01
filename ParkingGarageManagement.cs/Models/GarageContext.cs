using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.Entities;

namespace ParkingGarageManagement.cs.Models
{
	public class GarageContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Lot> Lots { get; set; }
		public DbSet<VehicleType> VehicleTypes { get; set; }
		public DbSet<Class> Classes { get; set; }
		public DbSet<TicketClass> TicketClasses { get; set; }

		public GarageContext(DbContextOptions<GarageContext> options) : base(options)
		{

		}
	}
}
