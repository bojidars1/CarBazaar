using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarBazaar.Data.Configurations
{
	public class UserCarListingConfiguration : IEntityTypeConfiguration<UserCarListing>
	{
		public void Configure(EntityTypeBuilder<UserCarListing> builder)
		{
			builder.HasKey(uc => new { uc.UserId, uc.CarListingId });

			builder.HasOne(uc => uc.User)
				   .WithMany()
				   .HasForeignKey(uc => uc.UserId);

			builder.HasOne(uc => uc.CarListing)
				   .WithMany()
				   .HasForeignKey(uc => uc.CarListingId);
		}
	}
}