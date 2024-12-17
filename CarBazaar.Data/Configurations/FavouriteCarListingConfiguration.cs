using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CarBazaar.Data.Configurations
{
	public class FavouriteCarListingConfiguration : IEntityTypeConfiguration<FavouriteCarListing>
	{
		public void Configure(EntityTypeBuilder<FavouriteCarListing> builder)
		{
			builder.HasKey(fc => new { fc.UserId, fc.CarListingId });

			builder.HasOne(fc => fc.User)
				.WithMany()
				.HasForeignKey(fc => fc.UserId);

			builder.HasOne(fc => fc.CarListing)
				.WithMany()
				.HasForeignKey(fc => fc.CarListingId);
		}
	}
}