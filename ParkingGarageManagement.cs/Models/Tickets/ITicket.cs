using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Tickets
{
	public interface ITicket
	{
		IEnumerable<int> LotsPositions { get; set; }
		int MaxHeight { get; set; }
		int MaxWidth { get; set; }
		int MaxLength { get; set; }
		string Classes { get; set; }
		int TimeLimit { get; set; }
		int Cost { get; set; }

	}
}
