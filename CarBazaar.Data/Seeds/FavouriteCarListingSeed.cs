using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;

namespace CarBazaar.Data.Seeds
{
	public class FavouriteCarListingSeed : IEntityTypeConfiguration<FavouriteCarListing>
	{
		public void Configure(EntityTypeBuilder<FavouriteCarListing> builder)
		{
			builder.HasData(new FavouriteCarListing
			{
				UserId = "user-1",
				CarListingId = CarListingSeed.carListing2Id
			});
		}
	}
}