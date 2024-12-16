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
		private readonly IServer server;

		public RedisRepository(IConnectionMultiplexer redis)
		{
			database = redis.GetDatabase();
			var endpoints = redis.GetEndPoints();
			server = redis.GetServer(endpoints.First());
		}

		public async Task AddToBlackListAsync(string token, TimeSpan expiry)
		{
			await database.StringSetAsync(token, "blacklisted", expiry);
		}

		public async Task<bool> IsBlackListedAsync(string token)
		{
			return await database.KeyExistsAsync(token);
		}
	}
}