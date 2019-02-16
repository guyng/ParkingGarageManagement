using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;
using ParkingGarageManagement.cs.Repositories.Abstract;
using ParkingGarageManagement.cs.Validators;
using TicketType = ParkingGarageManagement.cs.Infrastructure.Enums.TicketType;

namespace ParkingGarageManagement.cs.Controllers
{
	[Route("api/[controller]")]
	public class ParkingController : ControllerBase
	{
		private readonly IRepository<Vehicle> _vehicleRepository;
		private readonly IRepository<VehicleType> _vehicleTypeRepository;
		private readonly IRepository<Lot> _lotRepository;
		private readonly IRepository<LotRange> _lotRangeRepository;
		private readonly IRepository<Ticket> _ticketRepository;
		private readonly IRepository<Person> _peopleRepository;

		public ParkingController(IRepository<Vehicle> vehicleRepository,
			IRepository<VehicleType> vehicleTypeRepository, IRepository<Ticket> ticketRepository,
			IRepository<Lot> lotRepository, IRepository<LotRange> lotRangeRepository, IRepository<Person> peopleRepository)
		{
			_vehicleRepository = vehicleRepository;
			_vehicleTypeRepository = vehicleTypeRepository;
			_ticketRepository = ticketRepository;
			_lotRepository = lotRepository;
			_lotRangeRepository = lotRangeRepository;
			_peopleRepository = peopleRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetPersonVehicles(string personTz)
		{
			try
			{
				var person = await _peopleRepository.Query().SingleOrDefaultAsync(p => p.PersonTz == personTz);
				if (person != null)
				{
					var personVehicles = await _vehicleRepository.Query().Include(v => v.VehicleType).Where(v => v.PersonId == person.Id).ToListAsync();
					if (personVehicles != null)
					{
						var vehicleIdTypes = new List<dynamic>();
						foreach (var vehicle in personVehicles)
						{
							vehicleIdTypes.Add(new { VehicleId = vehicle.Id, VehicleType = vehicle.VehicleType.Name });
						}
						return Ok(vehicleIdTypes);
					}
				}			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> CheckInVehicle([FromBody]CheckInData checkIn)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var dimensionData = checkIn.VehicleDimensionData;
				var ticket = await _ticketRepository.Query().Include(t => t.TicketType).SingleOrDefaultAsync(c => c.Name == checkIn.TicketType.ToString());
				if (ticket == null)
				{
					return BadRequest();
				}
				var lastLotNumber = await _lotRepository.Query().Where(l => l.Vehicle.Ticket == ticket)
					.Select(l => l.LotPosition).OrderByDescending(c => c).Take(1)
					.FirstOrDefaultAsync();
				var lotRange = await _lotRangeRepository.Query().SingleOrDefaultAsync(lr => lr.TicketId == ticket.Id);
				if (!IsAvailableLot(ticket, lastLotNumber, lotRange?.MaxRange))
				{
					return BadRequest("No available lot.");
				}

				if (!VehicleDimensionValidator.Validate(dimensionData, ticket))
				{
					var alternativeTickets = await _ticketRepository.Query().Where(t => (t.MaxHeight > dimensionData.Height &&
																				  t.MaxWidth > dimensionData.Width &&
																				  t.MaxLength > dimensionData.Length) || t.TicketType.Type == TicketType.Vip).ToListAsync();
					var result = new { ChosenTicket = ticket, alternativeTickets };
					return Ok(result);
				}

				var person = await _peopleRepository.Query()
					.SingleOrDefaultAsync(p => p.PersonTz == checkIn.PersonData.PersonId);
				if (person == null)
				{
					person = MapPersonEntity(checkIn.PersonData);
					await _peopleRepository.InsertAsync(person);
				}

				var vehicle = await MapVehicleEntity(checkIn);
				vehicle.PersonId = person.Id;
				await _vehicleRepository.InsertAsync(vehicle);

				int lotPosition = lastLotNumber == 0 && lotRange != null ? lotRange.MinRange
								: lastLotNumber + 1;
				Lot lot = new Lot
				{
					Vehicle = vehicle,
					CheckIn = DateTime.Now,
					LotPosition = lotPosition
				};
				await _lotRepository.InsertAsync(lot);
				return Ok(StatusCodes.Status202Accepted);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return BadRequest();
			}

		}

		[HttpPost]
		public async Task<IActionResult> CheckOutVehicle([FromBody] CheckOutData checkOut)
		{
			var vehicle = await _vehicleRepository.Query().Include(v => v.Ticket)
				.Include(t => t.Ticket.TicketType)
				.SingleOrDefaultAsync
				(v =>
				v.PersonId == checkOut.PersonId && v.VehicleType.Name == checkOut.VehicleType.ToString());
			var lot = await _lotRepository.Query().SingleOrDefaultAsync(l => l.Vehicle == vehicle);
			var checkInOutHoursDiff = (checkOut.CheckOut - lot.CheckIn).TotalHours;
			var notPermittedParkHours = checkInOutHoursDiff - vehicle.Ticket.TimeLimit;
			var exceededTimeLimit =
				notPermittedParkHours > 0 && vehicle.Ticket.TicketType.Type != TicketType.Vip;
			if (exceededTimeLimit)
			{
				var priceToPay = notPermittedParkHours * vehicle.Ticket.Cost;
				var result = new {AmountOfParkedHours = checkInOutHoursDiff, priceToPay};
				return Ok(result);
			}

			return Ok(checkInOutHoursDiff);
		}

		//TODO: Add services, Use tasks everywhere, refactor the code.
		private bool IsAvailableLot(Ticket ticket, int lastLotNumber, int? maxLotRange)
		{
			return maxLotRange != null && maxLotRange.Value > lastLotNumber + 1;
		}

		private async Task<Vehicle> MapVehicleEntity(CheckInData checkInData)
		{
			var resultVehicle = new Vehicle
			{
				Ticket = await _ticketRepository.Query().SingleOrDefaultAsync(c => c.Name == checkInData.TicketType.ToString()),
				VehicleHeight = checkInData.VehicleDimensionData.Height.GetValueOrDefault(),
				VehicleWidth = checkInData.VehicleDimensionData.Width.GetValueOrDefault(),
				VehicleLength = checkInData.VehicleDimensionData.Length.GetValueOrDefault(),
				VehicleType = await _vehicleTypeRepository.Query()
				.SingleOrDefaultAsync(vt => vt.Name == checkInData.VehicleType.ToString())
			};
			return resultVehicle;
		}

		private Person MapPersonEntity(PersonData personData)
		{
			var resultPerson = new Person
			{
				Name = personData.Name,
				PersonTz = personData.PersonId,
				Phone = personData.Phone
			};
			return resultPerson;
		}
	}
}