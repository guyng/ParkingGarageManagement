using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;

namespace ParkingGarageManagement.cs.Services.Interfaces
{
    public interface IParkingService
    {
	    Task<List<string>> GetCompletingPeopleTz(string inputPersonTz);
	    Task<List<Vehicle>> GetPersonCheckedInVehiclesByTz(string personTz);
	    Task<List<Person>> GetListOfLateParkingPeople(DateTime inputDate);
	    Task<dynamic> GetParkLotState();

	    Task<dynamic> CheckInVehicle(CheckInData checkIn);

	    Task<dynamic> CheckOutVehicle(CheckOutData checkOut);

	    Task<List<Tuple<Vehicle, Lot>>> CreateRandomVehicles(int count);


    }
}
