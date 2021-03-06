﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParkingGarageManagement.cs.Models;

namespace ParkingGarageManagement.cs.Repositories.Abstract
{
	public class Repository<T> : IRepository<T>, IDisposable where T : class
	{
		public DbSet<T> Table { get; set; }
		private readonly DbContext _garageContext;
		public Repository(GarageContext garageContext)
		{
			_garageContext = garageContext;
			Table = garageContext.Set<T>();
		}

		public IQueryable<T> Query()
		{
			return Table.AsQueryable();
		}


		public async Task<T> GetAsync(int id)
		{
			return await _garageContext.FindAsync<T>(id);
		}

		public async Task InsertAsync(T entity)
		{
			try
			{
				await Table.AddAsync(entity);
				await _garageContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
		}

		public async Task UpdateAsync(T entity)
		{
			_garageContext.Entry(entity).State = EntityState.Modified;
			await _garageContext.SaveChangesAsync();
		}

		public async Task RemoveAsync(T entity)
		{
			_garageContext.Remove(entity);
			await _garageContext.SaveChangesAsync();
		}

		public async Task<List<T>> FromSql(string sqlQuery,object param)
		{
			List<T> result = null;
			try
			{
				object[] sqlParams = generateSqlParameters(param, ref sqlQuery);
				result = await Table.FromSql(sqlQuery, sqlParams).ToListAsync();
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



		public void Dispose()
		{
			_garageContext?.Dispose();
		}


	}
}
