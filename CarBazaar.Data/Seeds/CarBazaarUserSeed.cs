using CarBazaar.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CarBazaar.Data.Seeds
{
	public class CarBazaarUserSeed : IEntityTypeConfiguration<CarBazaarUser>
	{
		public void Configure(EntityTypeBuilder<CarBazaarUser> builder)
		{
			PasswordHasher<CarBazaarUser> ph = new PasswordHasher<CarBazaarUser>();
			var adminUser = new CarBazaarUser
			{
				Id = "admin-user",
				UserName = "admin@carbazaar.com",
				NormalizedUserName = "ADMIN@CARBAZAAR.COM",
				Email = "admin@carbazaar.com",
				NormalizedEmail = "ADMIN@CARBAZAAR.COM",
			};
			adminUser.PasswordHash = ph.HashPassword(adminUser, "admin123!");

			var user1 = new CarBazaarUser
			{
				Id = "user-1",
				UserName = "bojidar.stoi@gmail.com",
				NormalizedUserName = "BOJIDAR.STOI@GMAIL.COM",
				Email = "bojidar.stoi@gmail.com",
				NormalizedEmail = "BOJIDAR.STOI@GMAIL.COM",
			};
			user1.PasswordHash = ph.HashPassword(user1, "user123!");

			var user2 = new CarBazaarUser
			{
				Id = "user-2",
				UserName = "john.wick@abv.bg",
				NormalizedUserName = "JOHN.WICK@ABV.BG",
				Email = "john.wick@abv.bg",
				NormalizedEmail = "JOHN.WICK@ABV.BG",
			};
			user2.PasswordHash = ph.HashPassword(user2, "user123!");

			builder.HasData(adminUser, user1, user2);
		}
	}
}