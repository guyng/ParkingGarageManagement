using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Repositories.Abstract;

namespace ParkingGarageManagement.cs.Repositories
{
	public class PeopleRepository : Repository<Person>
	{
		public PeopleRepository(GarageContext garageContext) : base(garageContext)
		{
		}

		
	}
}
