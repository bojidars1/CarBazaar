using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Favourites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class FavouriteCarListingService(IFavouriteCarListingRepository repository) : IFavouriteCarListingService
	{
		public async Task<FavouriteCarListingPaginatedDto> GetFavouritesAsync(string userId, int pageIndex = 1, int pageSize = 10)
		{
			var query = repository.GetBaseQuery();
			query = query.Where(fc => fc.UserId == userId).Include(fc => fc.CarListing);

			var listings = await repository.GetPaginatedAsync(pageIndex, pageSize, query);
			int totalPages = listings.TotalPages;

			var items = listings.Select(fc => new FavouriteCarListingListDetailsDto
			{
				Id = fc.CarListingId,
				Name = fc.CarListing.Name,
				Price = fc.CarListing.Price,
				ImageURL = fc.CarListing.ImageURL
			}).ToList();

			return new FavouriteCarListingPaginatedDto
			{
				Items = items,
				TotalPages = totalPages,
			};
		}
	}
}