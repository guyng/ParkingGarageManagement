using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingGarageManagement.cs.Repositories.Abstract
{
	public interface IRepository<T>
	{
		IQueryable<T> Query();

		Task<T> GetAsync(int id);

		Task InsertAsync(T entity);

		Task UpdateAsync(T entity);
	}
}
