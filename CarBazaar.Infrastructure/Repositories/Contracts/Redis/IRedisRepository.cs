﻿using System;
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

		public Task StoreRefreshTokenAsync(string userId, string refreshToken, TimeSpan expiry);

		public Task<string?> GetUserIdByRefreshTokenAsync(string refreshToken);

		public Task RemoveRefreshTokenAsync(string refreshToken);
	}
}