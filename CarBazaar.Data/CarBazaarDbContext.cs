using CarBazaar.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data
{
    public class CarBazaarDbContext : IdentityDbContext<CarBazaarUser>
    {
        public CarBazaarDbContext(DbContextOptions<CarBazaarDbContext> options) : base(options) { }

        public DbSet<CarListing> CarListings { get; set; }

		public DbSet<UserCarListing> UserCarListings { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserCarListing>()
				.HasKey(uc => new { uc.UserId, uc.CarListingId });

			modelBuilder.Entity<UserCarListing>()
				.HasOne(uc => uc.User)
				.WithMany()
				.HasForeignKey(uc => uc.UserId);

			modelBuilder.Entity<UserCarListing>()
				.HasOne(uc => uc.CarListing)
				.WithMany()
				.HasForeignKey(uc => uc.CarListingId);

			modelBuilder.Entity<CarListing>().HasQueryFilter(cl => !cl.IsDeleted);
		}
	}
}