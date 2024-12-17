using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Seeds
{
	public class CarListingSeed : IEntityTypeConfiguration<CarListing>
	{
		public void Configure(EntityTypeBuilder<CarListing> builder)
		{
			builder.HasData(
				new CarListing
				{
					Id = Guid.NewGuid(),
					SellerId = "user-1",
					Name = "Toyota Corolla",
					Type = "Sedan",
					Brand = "Toyota",
					Price = 15000,
					Gearbox = "Automatic",
					State = "Used",
					Km = 80000,
					ProductionYear = 2018,
					Horsepower = 130,
					Color = "White",
					ExtraInfo = "Well maintained, low mileage.",
					ImageURL = "https://scene7.toyota.eu/is/image/toyotaeurope/cors0005a_web_2023:Medium-Landscape?ts=1708962012070&resMode=sharp2&op_usm=1.75,0.3,2,0",
				}
			);
		}
	}
}