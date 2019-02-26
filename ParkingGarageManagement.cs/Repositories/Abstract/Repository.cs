using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
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

		public async Task RemoveAsync(T entity)
		{
			GarageContext.Remove(entity);
			await GarageContext.SaveChangesAsync();
		}

		public async Task<List<T>> FromSql(string sqlQuery,object param)
		{
			List<T> result = null;
			try
			{
				object[] sqlParams = generateSqlParameters(param, ref sqlQuery);
				result = await RepositoryDbSet.FromSql(sqlQuery, sqlParams).ToListAsync();
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return result;
		}

		private object[] generateSqlParameters(object param,ref string sqlQuery)
		{
			sqlQuery += " ";
			List<object> sqlParams = new List<object>();
			foreach (var prop in param.GetType().GetProperties())
			{
				var propName = $"@{prop.Name}";
				var propValue = prop.GetValue(param, null);
				sqlQuery += propName;
				var sqlParam = new SqlParameter(propName, propValue);
				sqlParams.Add(sqlParam);
			}

			return sqlParams.ToArray();
		}
	}
}
