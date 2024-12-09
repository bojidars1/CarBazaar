using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services;
using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CarBazaarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarBazaarDbContext"));
});
builder.Services.AddControllers();

// Identity
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]));
        var issuer = builder.Configuration["JWT:Issuer"];
        var audience = builder.Configuration["JWT:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = key
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<CarBazaarUser>(options =>
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
    .AddEntityFrameworkStores<CarBazaarDbContext>();

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICarListingRepository, CarListingRepository>();
builder.Services.AddScoped<IUserCarListingService, UserCarListingService>();

// Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICarListingService, CarListingService>();
builder.Services.AddScoped<IUserCarListingRepository, UserCarListingRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MySpecificOrigins", builder =>
    {
        builder.WithOrigins("https://localhost", "https://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<CarBazaarUser>();

app.MapControllers();

app.UseCors("MySpecificOrigins");

app.MapFallbackToFile("/index.html");

app.Run();