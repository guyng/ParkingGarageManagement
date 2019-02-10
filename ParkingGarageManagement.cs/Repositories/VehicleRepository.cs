using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Repositories.Abstract;

namespace ParkingGarageManagement.cs.Repositories
{
	public class VehicleRepository : Repository<Vehicle>
	{
		public VehicleRepository(GarageContext garageContext) : base(garageContext)
		{
		}
	}
}
