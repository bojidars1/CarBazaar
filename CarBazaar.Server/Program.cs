using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Middleware;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Infrastructure.Repositories.Contracts.Redis;
using CarBazaar.Infrastructure.Repositories.Redis;
using CarBazaar.Server.Hubs;
using CarBazaar.Services;
using CarBazaar.Services.Contracts;
using CarBazaar.Services.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Extensions
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddAuthenticationConfig(builder.Configuration);
builder.Services.AddCorsConfig();
builder.Services.AddSwaggerConfig();

// Identity
builder.Services.AddIdentity<CarBazaarUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;

	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;

	options.SignIn.RequireConfirmedAccount = false;
	options.SignIn.RequireConfirmedEmail = false;
	options.SignIn.RequireConfirmedPhoneNumber = false;
})
    .AddEntityFrameworkStores<CarBazaarDbContext>()
    .AddDefaultTokenProviders();


//Authorization Policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdministratorRole", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Administrator");
    })
	.SetDefaultPolicy(new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)   
    .RequireAuthenticatedUser()
    .Build());

// Redis
string redisConString = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConString));

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICarListingRepository, CarListingRepository>();
builder.Services.AddScoped<IUserCarListingRepository, UserCarListingRepository>();
builder.Services.AddScoped<IFavouriteCarListingRepository, FavouriteCarListingRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IRedisRepository, RedisRepository>();

// Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICarListingService, CarListingService>();
builder.Services.AddScoped<IUserCarListingService, UserCarListingService>();
builder.Services.AddScoped<IFavouriteCarListingService, FavouriteCarListingService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IRedisService, RedisService>();

// SignalR
builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<CarBazaarUser>>();

    await RoleSeeder.SeedRolesAsync(roleManager);
    await RoleSeeder.AssignAdminRoleToUser(userManager, "admin123@gmail.com");
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtBlacklistValidationMiddleware>();
app.UseMiddleware<WebSocketsMiddleware>();

app.UseHttpsRedirection();

app.UseCors("MySpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chathub");
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();