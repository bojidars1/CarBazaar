using CarBazaar.Infrastructure.Repositories.Contracts.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Redis
{
	public class RedisRepository : IRedisRepository
	{
		public Task AddToBlackListAsync(string token, TimeSpan expiry)
		{
			throw new NotImplementedException();
		}

		public Task<bool> IsBlackListedAsync(string token)
		{
			throw new NotImplementedException();
		}
	}
}