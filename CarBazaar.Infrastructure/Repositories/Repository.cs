using CarBazaar.Data;
using CarBazaar.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly CarBazaarDbContext _context;
		private readonly DbSet<T> _dbSet;

        public Repository(CarBazaarDbContext context)
        {
            _context = context;
			_dbSet = context.Set<T>();
        }

        public Task AddAsync(T entity)
		{
			throw new NotImplementedException();
		}

		public Task DeleteByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task<List<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T?> GetByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(T entity)
		{
			throw new NotImplementedException();
		}
	}
}