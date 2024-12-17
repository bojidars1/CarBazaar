using CarBazaar.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data.Configurations
{
	public class CarListingConfiguration : IEntityTypeConfiguration<CarListing>
	{
		public void Configure(EntityTypeBuilder<CarListing> builder)
		{
			builder.HasKey(cl => cl.Id);

			builder.HasOne(cl => cl.Seller)
				   .WithMany()
				   .HasForeignKey(cl => cl.SellerId)
				   .OnDelete(DeleteBehavior.Restrict);

			builder.HasQueryFilter(cl => !cl.IsDeleted);
		}
	}
}