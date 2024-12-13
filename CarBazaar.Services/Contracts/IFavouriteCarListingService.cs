using CarBazaar.Data.Models;
using CarBazaar.ViewModels.Favourites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
	public interface IFavouriteCarListingService
	{
		Task<FavouriteCarListingPaginatedDto> GetFavouritesAsync(string userId, int pageIndex = 1, int pageSize = 10);

		Task<bool> AddToFavouriteAsync(string carId, string userId);

		Task<bool> DeleteFavouriteAsync(string carId, string userId);
	}
}