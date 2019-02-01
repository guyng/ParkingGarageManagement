using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Models;

namespace ParkingGarageManagement.cs.Repositories.Abstract
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly DbSet<T> RepositoryDbSet;
		private readonly DbContext GarageContext;
		public Repository(GarageContext garageContext)
		{
			GarageContext = garageContext;
			RepositoryDbSet = garageContext.Set<T>();
		}

		public IQueryable<T> Query()
		{
			return RepositoryDbSet.AsQueryable();
		}

		public async Task<T> GetAsync(int id)
		{
			return await GarageContext.FindAsync<T>(id);
		}

		public async Task InsertAsync(T entity)
		{
			try
			{
				await RepositoryDbSet.AddAsync(entity);
				await GarageContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}

		public async Task UpdateAsync(T entity)
		{
			GarageContext.Entry(entity).State = EntityState.Modified;
			await GarageContext.SaveChangesAsync();
		}
	}
}
