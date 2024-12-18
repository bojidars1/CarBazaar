﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarBazaar.Data.Models
{
    public class CarListing
    {
        [Key]
        [Comment("Car Listing Identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Seller Id")]
        public string SellerId { get; set; } = null!;

        [ForeignKey(nameof(SellerId))]
        [Comment("Seller")]
        public CarBazaarUser Seller { get; set; } = null!;

        [Comment("Car Listing Name")]
        public string Name { get; set; } = null!;

        [Comment("Car Type")]
        public string Type { get; set; } = null!;

        [Comment("Car Brand")]
        public string Brand { get; set; } = null!;

        [Comment("Car Price")]
        public decimal Price { get; set; }

        [Comment("Car Gearbox")]
        public string Gearbox { get; set; } = null!;

        [Comment("Car State")]
        public string State { get; set; } = null!;

        [Comment("Car KM")]
        public long Km { get; set; }

        [Comment("Car Production Year")]
        public int ProductionYear { get; set; }

        [Comment("Car Horsepower")]
        public int Horsepower { get; set; }

        [Comment("Car Color")]
        public string Color { get; set; } = null!;

        [Comment("Extra Car Info")]
        public string ExtraInfo { get; set; } = null!;

        [Comment("Image URL")]
        public string ImageURL { get; set; } = null!;

        [Comment("Listing Publication Date")]
        public DateTime PublicationDate { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}