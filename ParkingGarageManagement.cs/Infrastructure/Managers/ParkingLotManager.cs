using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Infrastructure.Managers
{
	public class ParkingLotManager
	{
		// Lot Id, Vehicle Id
		public static ConcurrentDictionary <int,int> ParkingLotVehicles = new ConcurrentDictionary<int,int>();
	}
}
