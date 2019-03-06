using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using ParkingGarageManagement.cs.Infrastructure.Managers.TPL;
using ParkingGarageManagement.cs.Models;
using ParkingGarageManagement.cs.Models.Domain;
using ParkingGarageManagement.cs.Models.DTO;
using ParkingGarageManagement.cs.Repositories.Abstract;
using ParkingGarageManagement.cs.Services.Interfaces;
using ParkingGarageManagement.cs.Validators;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using TicketType = ParkingGarageManagement.cs.Infrastructure.Enums.TicketType;

namespace ParkingGarageManagement.cs.Controllers
{
	[Route("api/[controller]")]
	public class ParkingController : ControllerBase
	{
		private readonly IParkingService _parkingService;

		public ParkingController(IParkingService parkingService)
		{
			_parkingService = parkingService;
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetPersonIds(string inputPersonTz)
		{
			try
			{
				var peopleTz = await _parkingService.GetCompletingPeopleTz(inputPersonTz);
				return Ok(peopleTz);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetPersonCheckedInVehicles(string personTz)
		{
			try
			{
				var personVehicles = await _parkingService.GetPersonCheckedInVehiclesByTz(personTz);		
				if (personVehicles != null)
				{
					var vehicleIdTypes = new List<dynamic>();
					foreach (var vehicle in personVehicles)
					{
						vehicleIdTypes.Add(new { VehicleId = vehicle.Id, VehicleType = vehicle.VehicleType.Name });
					}
					return Ok(vehicleIdTypes);
				}		
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return BadRequest();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetListOfLateParkingPeople(DateTime inputDate)
		{
			var listOfPeople = await _parkingService.GetListOfLateParkingPeople(inputDate);
			if (listOfPeople != null)
			{
				return Ok(listOfPeople);
			}

			return NotFound();
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> GetParkLotState()
		{
			var result = await _parkingService.GetParkLotState();
			if (result != null)
			{
				return Ok(result);
			}

			return BadRequest();
		}

		[HttpPost]
		public async Task<IActionResult> CheckInVehicle([FromBody]CheckInData checkIn)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var result = await _parkingService.CheckInVehicle(checkIn);
				if (result != null)
				{
					return Ok(result);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return BadRequest();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> CheckOutVehicle([FromBody] CheckOutData checkOut)
		{
			var result = await _parkingService.CheckOutVehicle(checkOut);
			if (result != null)
			{
				return Ok(result);
			}

			return NotFound();
		}


		//[HttpPost("[action]")]
		//public async Task<IActionResult> CreateRandomVehicles([FromBody]int count = 10)
		//{


		////	var estimated = Stopwatch.StartNew();
		////	var randomCreatedVehicles = await CreateVehicles(count);

		//	//var elapsed1 = $"1 elapsed: {estimated.Elapsed}";
		//	////double avgEstimated = 0;
		//	////long sumEstimated = 0;
		//	//estimated.Restart();
		//	//List<Vehicle> forloopList = new List<Vehicle>();
		//	//for (int i = 0; i < 30; i++)
		//	//{
		//	//	var randomCreatedVehicles = await CreateVehicles(count);
		//	//	forloopList.AddRange(randomCreatedVehicles);
		//	//}

		//	//var elapsed10 = $"10 elapsed: {estimated.Elapsed}";
		//	//estimated.Restart();

		//	//List<Vehicle> forloopListTPL = new List<Vehicle>();
		//	////await Task.Run(async() =>
		//	////{
		//	////	Parallel.For(0, 10, async d =>
		//	////	{
		//	////		var randomCreatedVehicles = await CreateVehicles(count, true);
		//	////		forloopListTPL.AddRange(randomCreatedVehicles);
		//	////	});
		//	////	await Task.Delay(1);
		//	////});

		//	//List<Task> tasks = new List<Task>();
		//	//for (int i = 0; i < 30; i++)
		//	//{
		//	//	tasks.Add(await Task.Factory.StartNew((async () =>
		//	//	{
		//	//		var randomCreatedVehicles = await CreateVehicles(count,true);
		//	//		forloopListTPL.AddRange(randomCreatedVehicles);
		//	//	})));
		//	//}



		//	//await Task.WhenAll(tasks);
		//	//var elapsed10fortpl = $"10 elapsed for tpl: {estimated.Elapsed}";
		//	//estimated.Stop();
		//	//if (randomCreatedVehicles != null)
		//	//{
		//	//	return Ok(randomCreatedVehicles);
		//	//}

		//	var elapsedFor = Stopwatch.StartNew();
		//	//for (int i = 0; i < 500; i++)
		//	//{
		//	//	await GetVehicles();
		//	//}

		//	//var elapsedForStr = $"for with await duration: {elapsedFor.Elapsed}";

		//	var elapsedFor2 = Stopwatch.StartNew();
		//	for (int i = 0; i < 500; i++)
		//	{
		//		await GetVehicles2();
		//	}

		//	var elapsedFor2Str = $"for2 with await duration: {elapsedFor2.Elapsed}";

		//	List<Task> tasks = new List<Task>();
		//	elapsedFor.Restart();
		//	for (int i = 0; i < 500; i++)
		//	{
		//		tasks.Add(Task.Run(GetVehicles));
		//	}
		//	await Task.WhenAll(tasks);
		//	var elapsedWhenAllStr = $"when all tasks duration: {elapsedFor.Elapsed}";


		//	tasks.Clear();
		//	elapsedFor.Restart();
		//	for (int i = 0; i < 500; i++)
		//	{
		//		tasks.Add(_executor.ExecuteInParallel(async () =>
		//		{

		//			using (GarageContext dbContext = new GarageContext(_options))
		//			{
		//				await dbContext.Vehicles.ToListAsync();
		//				await dbContext.Tickets.ToListAsync();
		//				await dbContext.Classes.ToListAsync();
		//				await dbContext.Persons.ToListAsync();
		//				await dbContext.LotRanges.ToListAsync();
		//			}
		//		}));
		//	}
		//	await Task.WhenAll(tasks);
		//	var elapsedWhenAllStr2 = $"when all2 tasks duration: {elapsedFor.Elapsed}";
		//	return BadRequest();
		//}

		//private async Task GetVehicles()
		//{
		//	using (GarageContext dbContext = new GarageContext(_options))
		//	{
		//		await dbContext.Vehicles.ToListAsync();
		//		await dbContext.Tickets.ToListAsync();
		//		await dbContext.Classes.ToListAsync();
		//		await dbContext.Persons.ToListAsync();
		//		await dbContext.LotRanges.ToListAsync();
		//	}
		//}

		//private async Task GetVehicles2()
		//{
		//	await _vehicleRepository.Query().ToListAsync();
		//	await _lotRepository.Query().ToListAsync();
		//	await _ticketRepository.Query().ToListAsync();
		//	await _lotRangeRepository.Query().ToListAsync();
		//	await _peopleRepository.Query().ToListAsync();
		//}

		[HttpPost("[action]")]
		public async Task<IActionResult> CreateRandomVehicles([FromBody] int count = 10)
		{
			var createdVehicles = await _parkingService.CreateRandomVehicles(count);
			if (createdVehicles != null)
			{
				return Ok(createdVehicles);
			}
			return BadRequest();
		}

	}
}