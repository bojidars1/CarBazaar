using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
	public class CarListingSearchDto
	{
		public string? Type { get; set; }

		public string? Brand { get; set; }

		public string? PriceRange { get; set; }
	}
}