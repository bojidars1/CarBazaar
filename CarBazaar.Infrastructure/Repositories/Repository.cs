using CarBazaar.Data;
using CarBazaar.Data.Models;
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

		public virtual async Task DeleteByIdAsync(string id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
			}
			await _context.SaveChangesAsync();
		}

		public virtual async Task<List<T>> GetAllAsync()
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public virtual IQueryable<T> GetBaseQuery()
		{
			return _dbSet.AsQueryable();
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