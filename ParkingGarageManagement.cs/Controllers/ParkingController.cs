using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;
using ParkingGarageManagement.cs.Models.Entities;
using ParkingGarageManagement.cs.Repositories.Abstract;

namespace ParkingGarageManagement.cs.Controllers
{
	[Route("api/[controller]")]
	public class ParkingController : Controller
	{
		private IRepository<Vehicle> _vehicleRepository;
		public ParkingController(IRepository<Vehicle> vehicleRepository)
		{
			_vehicleRepository = vehicleRepository;

		}

		[HttpGet]
		public IActionResult GetTest()
		{
			var ret = _vehicleRepository.Query().ToList();
			return Ok(ret);
		}

		[HttpPost]
		public async Task<IActionResult> CheckInVehicle([FromBody]CheckInData checkIn)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
	//			await _vehicleRepository.InsertAsync(vehicle);
				return Ok();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return BadRequest();
			}

		}
	}
}