using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class UserCarListingService(IUserCarListingRepository repository) : IUserCarListingService
	{
		public async Task AddAsync(UserCarListing listing)
		{
			await repository.AddAsync(listing);
		}

		public async Task<CarListingPaginatedSearchDto> GetListingsAsync(int pageIndex = 1, int pageSize = 10)
		{
			var listings = await repository.GetPaginatedAsync(pageIndex, pageSize);
			var totalPages = listings.TotalPages;

			var items = listings.Select(cl => new CarListingListDetailsDto{
				Id = cl.CarListingId,
				Name = cl.CarListing.Name,
				Price = cl.CarListing.Price,
				ImageURL = cl.CarListing.ImageURL,
			}).ToList();

			return new CarListingPaginatedSearchDto
			{
				Items = items,
				TotalPages = totalPages
			};
		}
	}
}