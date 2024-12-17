using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Seeds
{
	public class NotificationSeed : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.HasData(new Notification
			{
				Id = Guid.NewGuid(),
				UserId = "user-2",
				SenderId = "user-1",
				CarListingId = CarListingSeed.carListing2Id,
				Message = "You have a new message from bojidar.stoi@gmail.com."
			});
		}
	}
}