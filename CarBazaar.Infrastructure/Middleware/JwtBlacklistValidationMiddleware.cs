using CarBazaar.Infrastructure.Repositories.Contracts.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Middleware
{
	public class JwtBlacklistValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
	{
		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");

			var redisRepository = serviceProvider.GetRequiredService<IRedisRepository>();

			if (token != null && await redisRepository.IsBlackListedAsync(token))
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				await context.Response.WriteAsync("Token is blacklisted");
				return;
			}

			await next(context);
		}
	}
}