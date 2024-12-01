using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
	public class CarListingSearchDto
	{
		public string Type { get; set; } = null!;

		public string Brand { get; set; } = null!;

		public string PriceRange { get; set; } = null!;
	}
}