﻿using CarBazaar.Infrastructure.Repositories.Contracts.Redis;
using CarBazaar.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class RedisService(IRedisRepository repository) : IRedisService
	{
		public async Task AddToBlacklistAsync(string token, TimeSpan expiry)
		{
			await repository.AddToBlackListAsync(token, expiry);
		}

		public async Task<bool> IsBlackListedAsync(string token)
		{
			return await repository.IsBlackListedAsync(token);
		}
	}
}