using CarBazaar.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Favourites
{
	public class FavouriteCarListingPaginatedDto
	{
		public List<FavouriteCarListingListDetailsDto> Items { get; set; } = new List<FavouriteCarListingListDetailsDto>();

		public int TotalPages { get; set; }
	}
}