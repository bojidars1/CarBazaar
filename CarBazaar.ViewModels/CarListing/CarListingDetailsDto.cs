using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
    public class CarListingDetailsDto
    {
        public Guid Id { get; set; }

        public string SellerId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public decimal Price { get; set; }

        public string Gearbox { get; set; } = null!;

        public string State { get; set; } = null!;

        public long Km { get; set; }

        public int ProductionYear { get; set; }

        public int Horsepower { get; set; }

        public string Color { get; set; } = null!;

        public string ExtraInfo { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty;
    }
}