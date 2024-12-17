using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Seeds
{
	public class UserCarListingSeed : IEntityTypeConfiguration<UserCarListing>
	{
		public void Configure(EntityTypeBuilder<UserCarListing> builder)
		{
			var userCarListing1 = new UserCarListing
			{
				UserId = "user-1",
				CarListingId = CarListingSeed.carListing1Id
			};

			var userCarListing2 = new UserCarListing
			{
				UserId = "user-2",
				CarListingId = CarListingSeed.carListing2Id
			};

			builder.HasData(userCarListing1, userCarListing2);
		}
	}
}