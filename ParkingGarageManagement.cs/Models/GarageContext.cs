using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Models.Domain;

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
		public DbSet<TicketType> TicketTypes { get; set; }
		public DbSet<LotRange> LotRanges { get; set; }

		public GarageContext(DbContextOptions<GarageContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Person>(entity => {
				entity.HasIndex(e => e.PersonTz).IsUnique();
			});
		}
	}
}
