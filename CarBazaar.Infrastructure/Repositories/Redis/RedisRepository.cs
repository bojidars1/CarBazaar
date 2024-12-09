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

		public async Task<string?> GetUserIdByRefreshTokenAsync(string refreshToken)
		{
			var keys = server.Keys(pattern: "refreshToken:*");
			foreach (var key in keys)
			{
				if (await database.StringGetAsync(key) == refreshToken)
				{
					return key.ToString().Split(':')[1];
				}
			}
			return null;
		}

		public async Task<bool> IsBlackListedAsync(string token)
		{
			return await database.KeyExistsAsync(token);
		}

		public async Task RemoveRefreshTokenAsync(string refreshToken)
		{
			var keys = server.Keys(pattern: "refreshToken:*");
			foreach (var key in keys)
			{
				if (await database.StringGetAsync(key) == refreshToken)
				{
					await database.KeyDeleteAsync(key);
					break;
				}
			}
		}

		public async Task StoreRefreshTokenAsync(string userId, string refreshToken, TimeSpan expiry)
		{
			await database.StringSetAsync($"refreshToken:{userId}", refreshToken, expiry);
		}
	}
}