using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Seeds
{
	public class CarListingSeed : IEntityTypeConfiguration<CarListing>
	{
		public void Configure(EntityTypeBuilder<CarListing> builder)
		{
			var carListing1 = new CarListing
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
			};

			var carListing2 = new CarListing
			{
				Id = Guid.NewGuid(),
				SellerId = "user-2",
				Name = "BMW X5",
				Type = "SUV",
				Brand = "BMW",
				Price = 50000,
				Gearbox = "Automatic",
				State = "New",
				Km = 5000,
				ProductionYear = 2022,
				Horsepower = 300,
				Color = "Black",
				ExtraInfo = "Luxury package included.",
				ImageURL = "https://media.ed.edmunds-media.com/bmw/x5/2025/oem/2025_bmw_x5_4dr-suv_xdrive40i_fq_oem_1_1280.jpg"
			};

			builder.HasData(carListing1, carListing2);
		}
	}
}