using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.CarListing
{
	public class AddCarListingDto
	{
		[Required]
		public string Name { get; set; } = null!;

		[Required]
		public string Type { get; set; } = null!;

		[Required]
		public string Brand { get; set; } = null!;

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string Gearbox { get; set; } = null!;

		[Required]
		public string State { get; set; } = null!;

		[Required]
		public long Km { get; set; }

		[Required]
		public DateTime ProductionDate { get; set; }

		[Required]
		public int Horsepower { get; set; }

		[Required]
		public string Color { get; set; } = null!;

		[Required]
		public string ExtraInfo { get; set; } = null!;
	}
}