using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Contracts.Redis
{
	public interface IRedisRepository
	{
		public Task AddToBlackListAsync(string token, TimeSpan expiry);

		public Task<bool> IsBlackListedAsync(string token);
	}
}