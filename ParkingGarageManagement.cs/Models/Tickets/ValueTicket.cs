using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Models.Tickets
{
	public class ValueTicket : ITicket
	{
		public IEnumerable<int> LotsPositions { get; set; } = Enumerable.Range(11, 19);
		public int MaxHeight { get; set; } = 2500;
		public int MaxWidth { get; set; } = 2400;
		public int MaxLength { get; set; } = 5000;
		public string Classes { get; set; } = "A,B";
		public int TimeLimit { get; set; } = 72;
		public int Cost { get; set; } = 100;
	}
}
