using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Infrastructure.Enums;
using ParkingGarageManagement.cs.Models.Domain;
using VehicleType = ParkingGarageManagement.cs.Models.Domain.VehicleType;

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
		public DbSet<LotRange> LotRanges { get; set; }

		public GarageContext(DbContextOptions<GarageContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Person>(entity => {
				entity.HasIndex(e => e.PersonTz).IsUnique();
			});


			builder.Entity<Class>().HasData(
				new Class
				{
					Id = 1,
					Name = "A"
				},
				new Class
				{
					Id = 2,
					Name = "B"
				},
				new Class
				{
					Id = 3,
					Name = "C"
				}
			);

			builder.Entity<Person>().HasData(
				new Person
				{
					Id = 1,
					Name = "Guy",
					PersonTz = "302119282",
					Phone = "0504029472"
				},
				new Person
				{
					Id = 2,
					Name = "Duy",
					PersonTz = "304119282",
					Phone = "0504529472"
				},
				new Person
				{
					Id = 3,
					Name = "Ruy",
					PersonTz = "307119282",
					Phone = "0504929472"
				}
			);

			builder.Entity<VehicleType>().HasData(
				new VehicleType
				{
					Id = 1,
					ClassId = 1,
					Name = "Motorcycle"
				},
				new VehicleType
				{
					Id = 2,
					ClassId = 1,
					Name = "Private"
				},
				new VehicleType
				{
					Id = 3,
					ClassId = 1,
					Name = "Crossover"
				},
				new VehicleType
				{
					Id = 4,
					ClassId = 2,
					Name = "SUV"
				},
				new VehicleType
				{
					Id = 5,
					ClassId = 2,
					Name = "Van"
				},
				new VehicleType
				{
					Id = 6,
					ClassId = 3,
					Name = "Truck"
				}
			
			);

			builder.Entity<Vehicle>().HasData(
				new Vehicle
				{
					Id = 1,
					PersonId = 1,
					TicketId = 3,
					VehicleHeight = 1800,
					VehicleWidth = 1500,
					VehicleLength = 1200,
					VehicleTypeId = 1
				},
				new Vehicle
				{
					Id = 2,
					PersonId = 1,
					TicketId = 2,
					VehicleHeight = 2200,
					VehicleWidth = 1500,
					VehicleLength = 1200,
					VehicleTypeId = 2
				},
				new Vehicle
				{
					Id = 3,
					PersonId = 1,
					TicketId = 1,
					VehicleHeight = 2600,
					VehicleWidth = 1500,
					VehicleLength = 1200,
					VehicleTypeId = 3
				}
			);

			builder.Entity<LotRange>().HasData(
				new LotRange()
				{
					Id = 1,
					MaxRange = 10,
					MinRange = 1,
					TicketId = 1,
				},
				new LotRange()
				{
					Id = 2,
					MaxRange = 30,
					MinRange = 11,
					TicketId = 2,
				},
				new LotRange()
				{
					Id = 3,
					MaxRange = 60,
					MinRange = 31,
					TicketId = 3,
				}
			);

			builder.Entity<Lot>().HasData(
				new Lot()
				{
					CheckIn = DateTime.Now.AddDays(-2),
					Id = 1,
					VehicleId = 1,
					LotPosition = 1

				},
				new Lot()
				{
					CheckIn = DateTime.Now.AddDays(-1),
					Id = 2,
					VehicleId = 2,
					LotPosition = 11
				});

			builder.Entity<Ticket>().HasData(
				new Ticket
				{
					Id = 1,
					Name = "Vip",
					MaxHeight = -1,
					MaxLength = -1,
					MaxWidth = -1,
				    Cost = 200,
					TicketType = TicketType.Vip,
					TimeLimit = 72
				},
				new Ticket
				{
					Id = 2,
					Name = "Value",
					MaxHeight = 2500,
					MaxLength = 2500,
					MaxWidth = 2500,
					Cost = 100,
					TicketType = TicketType.Value,
					TimeLimit = 48
				},
				new Ticket
				{
					Id = 3,
					Name = "Regular",
					MaxHeight = 2000,
					MaxLength = 2000,
					MaxWidth = 2000,
					Cost = 50,
					TicketType = TicketType.Regular,
					TimeLimit = 24
				}
			);
		}
	}
}
