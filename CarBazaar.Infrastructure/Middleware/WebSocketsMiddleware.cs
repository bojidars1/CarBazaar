using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Middleware
{
	public class WebSocketsMiddleware(RequestDelegate next)
	{
		public async Task Invoke(HttpContext httpContext)
		{
			var request = httpContext.Request;

			if (request.Path.StartsWithSegments("/chathub", StringComparison.OrdinalIgnoreCase))
			{
				if (request.Query.TryGetValue("access_token", out var token)) {
					var tokenString = token.ToString();
					request.Headers.Add("Authorization", $"Bearer {token}");
				}
			}

			await next(httpContext);
		}
	}
}