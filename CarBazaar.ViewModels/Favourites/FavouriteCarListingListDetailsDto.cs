using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Favourites
{
	public class FavouriteCarListingListDetailsDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;

		public decimal Price { get; set; }

		public string ImageURL { get; set; } = string.Empty;
	}
}