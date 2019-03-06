using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Infrastructure.Enums;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;
using ParkingGarageManagement.cs.Repositories.Abstract;
using ParkingGarageManagement.cs.Validators;
using VehicleType = ParkingGarageManagement.cs.Models.Domain.VehicleType;

namespace ParkingGarageManagement.cs.Services
{
    public class ParkingService
    {
	    private readonly IRepository<Vehicle> _vehicleRepository;
	    private readonly IRepository<VehicleType> _vehicleTypeRepository;
	    private readonly IRepository<Lot> _lotRepository;
	    private readonly IRepository<LotRange> _lotRangeRepository;
	    private readonly IRepository<Ticket> _ticketRepository;
	    private readonly IRepository<Person> _peopleRepository;

	    public ParkingService(IRepository<Person> peopleRepository, IRepository<Ticket> ticketRepository, IRepository<LotRange> lotRangeRepository, IRepository<Lot> lotRepository, IRepository<VehicleType> vehicleTypeRepository, IRepository<Vehicle> vehicleRepository)
	    {
		    _peopleRepository = peopleRepository;
		    _ticketRepository = ticketRepository;
		    _lotRangeRepository = lotRangeRepository;
		    _lotRepository = lotRepository;
		    _vehicleTypeRepository = vehicleTypeRepository;
		    _vehicleRepository = vehicleRepository;
	    }

	    public Task<List<string>> GetCompletingPeopleTz(string inputPersonTz)
	    {
			return _peopleRepository.Query().Where(p => p.PersonTz.StartsWith(inputPersonTz)).Select(p => p.PersonTz)
				.ToListAsync();
		}

	    public async Task<List<Vehicle>> GetPersonCheckedInVehiclesByTz(string personTz)
	    {
			List<Vehicle> personVehicles = null;
		    var person = await _peopleRepository.Query().SingleOrDefaultAsync(p => p.PersonTz == personTz);
		    if (person != null)
		    {
			    var lots = await _lotRepository.Query().ToListAsync();
			    personVehicles = await _vehicleRepository.Query()
				    .Include(v => v.VehicleType)
				    .Where(v => v.PersonId == person.Id)
				    .Join(lots, v => v.Id, l => l.VehicleId, (v, l) => v)
				    .ToListAsync();
		    }

		    return personVehicles;
	    }

	    public Task<List<Person>> GetListOfLateParkingPeople(DateTime inputDate)
	    {
		   return _peopleRepository.FromSql("FindLateToPickupPeople", new { inputDate });
		}

	    public async Task<dynamic> GetParkLotState()
	    {
		    return await (from lot in _lotRepository.Table
			    join vehicle in _vehicleRepository.Table
				    on lot.VehicleId equals vehicle.Id
			    select new
			    {
				    VehicleId = vehicle.Id,
				    VehicleType = vehicle.VehicleType.Name,
				    lot.LotPosition
			    }).ToListAsync();
		}

	    public async Task<dynamic> CheckInVehicle(CheckInData checkIn)
	    {
		    var ticket = await _ticketRepository.Query().SingleOrDefaultAsync(c => c.TicketType == checkIn.TicketType);
		    if (ticket == null)
		    {
			    return null;
		    }
		    var lastLotNumber = await _lotRepository.Query().Where(l => l.Vehicle.Ticket == ticket)
			    .Select(l => l.LotPosition).OrderByDescending(c => c).Take(1)
			    .FirstOrDefaultAsync();
		    var lotRange = await _lotRangeRepository.Query().SingleOrDefaultAsync(lr => lr.TicketId == ticket.Id);
		    if (!IsAvailableLot(ticket, lastLotNumber, lotRange?.MaxRange))
		    {
			    return null;
		    }
		    var dimensionData = checkIn.VehicleDimensionData;
		    if (!VehicleDimensionValidator.Validate(dimensionData, ticket))
		    {
			    var alternativeTickets = await _ticketRepository.Query().Where(t => (t.MaxHeight > dimensionData.Height &&
			                                                                         t.MaxWidth > dimensionData.Width &&
			                                                                         t.MaxLength > dimensionData.Length) || t.TicketType == TicketType.Vip).ToListAsync();
			    var result = new { ChosenTicket = ticket, alternativeTickets };
			    return result;
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
			    CheckIn = DateTime.UtcNow,
			    LotPosition = lotPosition
		    };
		    await _lotRepository.InsertAsync(lot);
		    return StatusCodes.Status202Accepted;
		}

	    public async Task<dynamic> CheckOutVehicle(CheckOutData checkOut)
	    {
		    var vehicle = await _vehicleRepository.Query().Include(v => v.Ticket)
			    .SingleOrDefaultAsync
				    (v => v.Id == checkOut.VehicleId);
		    var lot = await _lotRepository.Query().SingleOrDefaultAsync(l => l.Vehicle == vehicle);
		    if (lot == null)
		    {
			    return null;
		    }
		    var checkInOutHoursDiff = (checkOut.Checkout - lot.CheckIn).TotalHours;
		    var notPermittedParkHours = checkInOutHoursDiff - vehicle.Ticket.TimeLimit;
		    var exceededTimeLimit =
			    notPermittedParkHours > 0 && vehicle.Ticket.TicketType != TicketType.Vip;
		    if (exceededTimeLimit)
		    {
			    var priceToPay = notPermittedParkHours * vehicle.Ticket.Cost;
			    var result = new { AmountOfParkedHours = checkInOutHoursDiff, priceToPay };
			    return result;
		    }

		    await _vehicleRepository.RemoveAsync(vehicle);
		    return new { AmountOfParkedHours = checkInOutHoursDiff };
		}

	    private async Task<List<Tuple<Vehicle, Lot>>> CreateRandomVehicles(int count)
	    {
			List<Tuple<Vehicle, Lot>> result = new List<Tuple<Vehicle, Lot>>();
			while (count-- > 0)
			{
				try
				{
					Vehicle vehicle = new Vehicle
					{
						TicketId = (await _ticketRepository.Query()
							.Skip(new Random()
								.Next(0, _ticketRepository.Table.Count(t => true))).Take(1).FirstAsync()).Id,
						PersonId = (await _peopleRepository.Query()
							.Skip(new Random()
								.Next(0, _peopleRepository.Table.Count(t => true))).Take(1).FirstAsync()).Id,
						VehicleTypeId = (await _vehicleTypeRepository.Query()
							.Skip(new Random()
								.Next(0, _vehicleTypeRepository.Table.Count(v => true))).Take(1).FirstAsync()).Id
					};
					var ticket = await _ticketRepository.Query().SingleOrDefaultAsync(t => t.Id == vehicle.TicketId);
					var lastLotNumber = await _lotRepository.Query().Where(l => l.Vehicle.Ticket == ticket)
						.Select(l => l.LotPosition).OrderByDescending(c => c).Take(1)
						.FirstOrDefaultAsync();
					var lotRange = await _lotRangeRepository.Query()
						.SingleOrDefaultAsync(lr => lr.TicketId == ticket.Id);
					if (!IsAvailableLot(ticket, lastLotNumber, lotRange?.MaxRange))
					{
						continue;
					}

					vehicle.VehicleHeight = new Random().Next(0, ticket.TicketType == TicketType.Vip ? int.MaxValue : ticket.MaxHeight);
					vehicle.VehicleWidth = new Random().Next(0, ticket.TicketType == TicketType.Vip ? int.MaxValue : ticket.MaxWidth);
					vehicle.VehicleLength = new Random().Next(0, ticket.TicketType == TicketType.Vip ? int.MaxValue : ticket.MaxLength);
					await _vehicleRepository.InsertAsync(vehicle);

					Lot lot = new Lot
					{
						VehicleId = vehicle.Id,
						CheckIn = DateTime.Now,
						LotPosition = lastLotNumber == 0 && lotRange != null ? lotRange.MinRange : lastLotNumber + 1
					};
					result.Add(Tuple.Create(vehicle, lot));
					await _lotRepository.InsertAsync(lot);
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}

			}
			return result;
		}




		/// <summary>
		/// 
		/// </summary>
		/// <param name="checkInData"></param>
		/// <returns></returns>
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

		private bool IsAvailableLot(Ticket ticket, int lastLotNumber, int? maxLotRange)
	    {
		    return maxLotRange != null && maxLotRange.Value > lastLotNumber + 1;
	    }
	}
}
