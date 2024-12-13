using CarBazaar.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Contracts
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync();
		
		Task<T?> GetByIdAsync(string id);

		Task DeleteAsync(T entity);

		Task AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteByIdAsync(string id);

		IQueryable<T> GetBaseQuery();

		Task<PaginatedList<T>> GetPaginatedAsync(int? pageIndex, int pageSize, IQueryable<T>? queryable = null);
	}
}