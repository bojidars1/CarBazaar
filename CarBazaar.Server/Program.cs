using CarBazaar.Data;
using CarBazaar.Infrastructure.Repositories;
using CarBazaar.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CarBazaarDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarBazaarDbContext"));
});
builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>);
builder.Services.AddScoped<ICarListingRepository, CarListingRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.UseCors("MySpecificOrigins");

app.MapFallbackToFile("/index.html");

app.Run();