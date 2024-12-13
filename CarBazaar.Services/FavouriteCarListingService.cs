using CarBazaar.Data.Models;
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
	public class FavouriteCarListingService(IFavouriteCarListingRepository favouriteRepository,
		ICarListingRepository carListingRepository) : IFavouriteCarListingService
	{
		public async Task<bool> AddToFavouriteAsync(string carId, string userId)
		{
			var carListing = await carListingRepository.GetByIdAsync(carId);
            if (carListing == null)
            {
				return false;
            }

			var favourite = await favouriteRepository.GetByUserAndCarIdAsync(carId, userId);
			if (favourite != null)
			{
				return false;
			}

			favourite = new FavouriteCarListing
			{
				UserId = userId,
				CarListingId = carListing.Id,
			};

			await favouriteRepository.AddAsync(favourite);
			return true;
        }

		public async Task<bool> DeleteFavouriteAsync(string carId, string userId)
		{
			var carListing = await carListingRepository.GetByIdAsync(carId);
			if (carListing == null)
			{
				return false;
			}

			var favourite = await favouriteRepository.GetByUserAndCarIdAsync(carId, userId);
			if (favourite == null)
			{
				return false;
			}

			await favouriteRepository.DeleteAsync(favourite);
			return true;
		}

		public async Task<FavouriteCarListingPaginatedDto> GetFavouritesAsync(string userId, int pageIndex = 1, int pageSize = 10)
		{
			var query = favouriteRepository.GetBaseQuery();
			query = query.Where(fc => fc.UserId == userId).Include(fc => fc.CarListing);

			var listings = await favouriteRepository.GetPaginatedAsync(pageIndex, pageSize, query);
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