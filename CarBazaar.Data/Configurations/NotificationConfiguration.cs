using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CarBazaar.Data.Configurations
{
	public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
	{
		public void Configure(EntityTypeBuilder<Notification> builder)
		{
			builder.HasKey(n => n.Id);

			builder.HasOne(n => n.User)
				.WithMany()
				.HasForeignKey(n => n.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(n => n.Sender)
				.WithMany()
				.HasForeignKey(n => n.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(n => n.CarListing)
				.WithMany()
				.HasForeignKey(n => n.CarListingId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}