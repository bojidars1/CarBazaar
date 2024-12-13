using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Favourites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class FavouriteCarListingService(IFavouriteCarListingRepository repository) : IFavouriteCarListingService
	{
		public async Task<FavouriteCarListingPaginatedDto> GetFavouritesAsync(string userId)
		{
			var query = repository.GetBaseQuery();
			query 
		}
	}
}