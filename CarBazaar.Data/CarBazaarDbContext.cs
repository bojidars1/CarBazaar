using CarBazaar.Data.Configurations;
using CarBazaar.Data.Models;
using CarBazaar.Data.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data
{
    public class CarBazaarDbContext : IdentityDbContext<CarBazaarUser>
    {
        public CarBazaarDbContext(DbContextOptions<CarBazaarDbContext> options) : base(options) { }

        public DbSet<CarListing> CarListings { get; set; }

		public DbSet<UserCarListing> UserCarListings { get; set; }

		public DbSet<FavouriteCarListing> FavoriteCarListings { get; set; }

		public DbSet<ChatMessage> ChatMessages { get; set; }

		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new CarListingConfiguration());
			modelBuilder.ApplyConfiguration(new UserCarListingConfiguration());
			modelBuilder.ApplyConfiguration(new FavouriteCarListingConfiguration());
			modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
			modelBuilder.ApplyConfiguration(new NotificationConfiguration());


			modelBuilder.ApplyConfiguration(new CarBazaarUserSeed());
			modelBuilder.ApplyConfiguration(new CarListingSeed());
			modelBuilder.ApplyConfiguration(new ChatMessageSeed());
			modelBuilder.ApplyConfiguration(new FavouriteCarListingSeed());
			modelBuilder.ApplyConfiguration(new NotificationSeed());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.ConfigureWarnings(warnings =>
			{
				warnings.Ignore(RelationalEventId.PendingModelChangesWarning);
			});

			base.OnConfiguring(optionsBuilder);
		}
	}
}