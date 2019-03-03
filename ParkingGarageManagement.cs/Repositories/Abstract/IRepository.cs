﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ParkingGarageManagement.cs.Repositories.Abstract
{
	public interface IRepository<T> where T: class
	{
		IQueryable<T> Query();

		Task<T> GetAsync(int id);

		Task InsertAsync(T entity);

		Task UpdateAsync(T entity);

		Task RemoveAsync(T entity);

		Task<List<T>> FromSql(string sqlQuery, object param);

		DbSet<T> Table { get; set; }

		DbContext _garageContext { get; set; }
	}
}
