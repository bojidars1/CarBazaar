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

		Task AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteByIdAsync(string id);
	}
}