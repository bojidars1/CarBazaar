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

        public virtual async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public virtual Task DeleteByIdAsync(string id)
		{
			throw new NotImplementedException();
		}

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public virtual async Task<T?> GetByIdAsync(string id)
		{
			if (Guid.TryParse(id, out var guidId))
			{
				return await _dbSet.FindAsync(guidId);
			} else
			{
				return null;
			}
			
		}

		public virtual async Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}