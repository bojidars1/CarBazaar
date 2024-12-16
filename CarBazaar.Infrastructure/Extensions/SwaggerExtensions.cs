using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarBazaar.Infrastructure.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "CarBazaarApi",
					Version = "v1"
				});

				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Type = SecuritySchemeType.ApiKey,
					Name = "Authorization",
					In = ParameterLocation.Header,
					Scheme = "Bearer",
					BearerFormat = "JWT"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						System.Array.Empty<string>()
					}
				});
			});
			return services;
		}
	}
}