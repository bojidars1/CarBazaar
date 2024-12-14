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

			if (request.Path.StartsWithSegments("/hub", StringComparison.OrdinalIgnoreCase) &&
				request.Query.TryGetValue("token", out var token))
			{
				request.Headers.Add("Authorization", $"Bearer {token}");
			}

			await next(httpContext);
		}
	}
}