using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Models;

namespace ParkingGarageManagement.cs.Infrastructure.Managers.TPL
{
	public class Executor : IExecutor
	{
		private DbContextOptions<GarageContext> _options;
		public Executor(DbContextOptions<GarageContext> options)
		{
			_options = options;
		}

		public async Task ExecuteInParallel(Action action)
		{
			 await Task.Run(action);

		}

	}
}
