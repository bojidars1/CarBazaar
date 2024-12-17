﻿using CarBazaar.Data.Models;
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

		public DbSet<FavouriteCarListing> FavoriteCarListings { get; set; }

		public DbSet<ChatMessage> ChatMessages { get; set; }

		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CarListing>()
				.HasKey(cl => new { cl.Id });

			modelBuilder.Entity<CarListing>()
				.HasOne(cl => cl.Seller)
				.WithMany()
				.HasForeignKey(cl => cl.SellerId)
				.OnDelete(DeleteBehavior.Restrict);

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

			modelBuilder.Entity<FavouriteCarListing>()
				.HasKey(fc => new { fc.UserId, fc.CarListingId });

			modelBuilder.Entity<FavouriteCarListing>()
				.HasOne(fc => fc.User)
				.WithMany()
				.HasForeignKey(fc => fc.UserId);

			modelBuilder.Entity<FavouriteCarListing>()
				.HasOne(fc => fc.CarListing)
				.WithMany()
				.HasForeignKey(fc => fc.CarListingId);

			modelBuilder.Entity<ChatMessage>()
				.HasKey(cm => cm.Id);

			modelBuilder.Entity<ChatMessage>()
				.HasOne(cm => cm.Sender)
				.WithMany()
				.HasForeignKey(cm => cm.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ChatMessage>()
				.HasOne(cm => cm.Receiver)
				.WithMany()
				.HasForeignKey(cm => cm.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ChatMessage>()
				.HasOne(cm => cm.CarListing)
				.WithMany()
				.HasForeignKey(cm => cm.CarListingId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Notification>()
				.HasKey(n => n.Id);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.User)
				.WithMany()
				.HasForeignKey(n => n.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.Sender)
				.WithMany()
				.HasForeignKey(n => n.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.CarListing)
				.WithMany()
				.HasForeignKey(n => n.CarListingId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CarListing>().HasQueryFilter(cl => !cl.IsDeleted);
		}
	}
}