using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingGarageManagement.cs.Models;

namespace ParkingGarageManagement.cs.Infrastructure.Managers.TPL
{
    public interface IExecutor
    {
	    Task ExecuteInParallel(Action action);
    }
}
