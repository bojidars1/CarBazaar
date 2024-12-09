using CarBazaar.Infrastructure.Repositories.Contracts.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Redis
{
	public class RedisRepository : IRedisRepository
	{
		private readonly IDatabase database;

		public RedisRepository(IConnectionMultiplexer redis)
		{
			database = redis.GetDatabase();
		}

		public async Task AddToBlackListAsync(string token, TimeSpan expiry)
		{
			await database.StringSetAsync(token, "blacklisted", expiry);
		}

		public async Task<bool> IsBlackListedAsync(string token)
		{
			return await database.KeyExistsAsync(token);
		}

		public async Task StoreRefreshTokenAsync(string userId, string refreshToken, TimeSpan expiry)
		{
			await database.StringSetAsync(userId, refreshToken, expiry);
		}
	}
}