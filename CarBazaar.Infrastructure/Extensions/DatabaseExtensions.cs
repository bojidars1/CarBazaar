using CarBazaar.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarBazaar.Infrastructure.Extensions
{
	public static class DatabaseExtensions
	{
		public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<CarBazaarDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("CarBazaarDbContext"));
			});
			return services;
		}
	}
}