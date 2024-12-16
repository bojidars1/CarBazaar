using Microsoft.Extensions.DependencyInjection;

namespace CarBazaar.Infrastructure.Extensions
{
	public static class CorsExtensions
	{
		public static IServiceCollection AddCorsConfig(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(name: "MySpecificOrigins", builder =>
				{
					builder.WithOrigins("https://localhost", "https://localhost:5173")
						.AllowCredentials()
						.AllowAnyMethod()
						.AllowAnyHeader()
						.SetIsOriginAllowedToAllowWildcardSubdomains();
				});
			});
			return services;
		}
	}
}