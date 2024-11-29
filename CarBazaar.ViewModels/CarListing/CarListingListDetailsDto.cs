using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.CarListing
{
	public class CarListingListDetailsDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public string ImageURL {  get; set; } = string.Empty;
	}
}