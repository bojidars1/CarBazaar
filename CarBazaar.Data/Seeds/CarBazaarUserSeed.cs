using CarBazaar.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Seeds
{
	public class CarBazaarUserSeed : IEntityTypeConfiguration<CarBazaarUser>
	{
		public void Configure(EntityTypeBuilder<CarBazaarUser> builder)
		{
			var adminUser = new CarBazaarUser
			{
				Id = "admin-user",
				UserName = "admin@carbazaar.com",
				NormalizedUserName = "ADMIN@CARBAZAAR.COM",
				Email = "admin@carbazaar.com",
				NormalizedEmail = "ADMIN@CARBAZAAR.COM",
			};
			adminUser.PasswordHash = new PasswordHasher<CarBazaarUser>().HashPassword(adminUser, "admin123!");
			

			var user1 = new CarBazaarUser
			{
				Id = "user-1",
				UserName = "bojidar.stoi@gmail.com",
				NormalizedUserName = "BOJIDAR.STOI@GMAIL.COM",
				Email = "bojidar.stoi@gmail.com",
				NormalizedEmail = "BOJIDAR.STOI@GMAIL.COM"
			};

			var user2 = new CarBazaarUser
			{
				Id = "user-2",
				UserName = "john.wick@abv.bg",
				NormalizedUserName = "JOHN.WICK@ABV.BG",
				Email = "john.wick@abv.bg",
				NormalizedEmail = "JOHN.WICK@ABV.BG"
			};

			builder.HasData(user1, user2);
		}
	}
}