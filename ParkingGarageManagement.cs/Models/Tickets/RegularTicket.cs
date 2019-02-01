using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Tickets
{
	public class RegularTicket : ITicket
	{
		public IEnumerable<int> LotsPositions { get; set; } = Enumerable.Range(31, 29);
		public int MaxHeight { get; set; } = 2000;
		public int MaxWidth { get; set; } = 2000;
		public int MaxLength { get; set; } = 3000;
		public string Classes { get; set; } = "A";
		public int TimeLimit { get; set; } = 24;
		public int Cost { get; set; } = 50;
	}
}
