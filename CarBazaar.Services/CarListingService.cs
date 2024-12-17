using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class CarListingService(ICarListingRepository repository, IUserCarListingService userCarListingService,
		UserManager<CarBazaarUser> userManager) : ICarListingService
	{
		public async Task AddAsync(AddCarListingDto dto, string userId)
		{
			CarListing carListing = new CarListing
			{
				Id = Guid.NewGuid(),
				Name = dto.Name,
				Type = dto.Type,
				Brand = dto.Brand,
				Price = dto.Price,
				Gearbox = dto.Gearbox,
				State = dto.State,
				Km = dto.Km,
				ProductionYear = dto.ProductionYear,
				Horsepower = dto.Horsepower,
				Color = dto.Color,
				ExtraInfo = dto.ExtraInfo,
				ImageURL = dto.ImageURL,
				PublicationDate = DateTime.Now
			};

			await repository.AddAsync(carListing);

			await userCarListingService.AddAsync(new UserCarListing
			{
				UserId = userId,
				CarListingId = carListing.Id,
			}); 
		}

		public async Task<bool> UpdateCarListingAsync(EditCarListingDto dto, string userId)
		{
			var user = await userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}

			var isUserAdmin = await userManager.IsInRoleAsync(user, "Administrator");
			if (!isUserAdmin)
			{
				UserCarListing? userCarListing = await userCarListingService.GetByCarIdAsync(dto.Id);
				if (userCarListing == null || userCarListing.UserId != userId)
				{
					return false;
				}
			}

            CarListing? listing = await repository.GetByIdAsync(dto.Id);
			if (listing == null)
			{
				return false;
			}

			listing.Name = dto.Name;
			listing.Type = dto.Type;
			listing.Brand = dto.Brand;
			listing.Price = dto.Price;
			listing.Gearbox = dto.Gearbox;
			listing.State = dto.State;
			listing.Km = dto.Km;
			listing.ProductionYear = dto.ProductionYear;
			listing.Horsepower = dto.Horsepower;
			listing.Color = dto.Color;
			listing.ExtraInfo = dto.ExtraInfo;
			listing.ImageURL = dto.ImageURL;

			await repository.UpdateAsync(listing);
			return true;
		}

		public async Task<List<CarListingListDetailsDto>> GetAllAsync()
		{
			var listings = await repository.GetAllAsync();
			return listings.Select(l => new CarListingListDetailsDto
			{
				Id = l.Id,
				Name = l.Name,
				Price = l.Price,
				ImageURL = l.ImageURL
			}).ToList();
		}

		public async Task<CarListingDetailsDto?> GetCarListingDetailsByIdAsync(string id)
		{
			var listing = await repository.GetByIdAsync(id);

			if (listing == null)
			{
				return null;
			}

			var userCar = await userCarListingService.GetByCarIdAsync(id);
			if (userCar == null)
			{
				return null;
			}

			return new CarListingDetailsDto
			{
				Id = listing.Id,
				SellerId = userCar.UserId,
				Name = listing.Name,
				Type = listing.Type,
				Brand = listing.Brand,
				Price = listing.Price,
				Gearbox = listing.Gearbox,
				State = listing.State,
				Km = listing.Km,
				ProductionYear = listing.ProductionYear,
				Horsepower = listing.Horsepower,
				Color = listing.Color,
				ExtraInfo = listing.ExtraInfo,
				ImageURL = listing.ImageURL
			};
		}

		public async Task<bool> SoftDeleteCarListingAsync(string id, string userId)
		{
			var user = await userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return false;
			}

			var isUserAdmin = await userManager.IsInRoleAsync(user, "Administrator");

			if (!isUserAdmin)
			{
				UserCarListing? userCarListing = await userCarListingService.GetByCarIdAsync(id);
				if (userCarListing == null || userCarListing.UserId != userId)
				{
					return false;
				}
			}

			CarListing? listing = await repository.GetByIdAsync(id);
			if (listing == null)
			{
				return false;
			}

			listing.IsDeleted = true;
			await repository.UpdateAsync(listing);
			return true;
		}

		public async Task<DeleteCarListingDto?> GetDeleteCarListingDtoByIdAsync(string id, string userId)
		{
			var user = await userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return null;
			}

			var isUserAdmin = await userManager.IsInRoleAsync(user, "Administrator");

			if (!isUserAdmin)
			{
				UserCarListing? userCarListing = await userCarListingService.GetByCarIdAsync(id);
				if (userCarListing == null || userCarListing.UserId != userId)
				{
					return null;
				}
			}

			var listing = await repository.GetByIdAsync(id);
			if (listing == null)
			{
				return null;
			}

			return new DeleteCarListingDto
			{
				Name = listing.Name,
			};
		}

		public async Task<CarListingPaginatedSearchDto> SearchCarListingsAsync(
			CarListingSearchDto dto, int? pageIndex, int pageSize)
		{
			var query = repository.GetBaseQuery();

			if (!string.IsNullOrEmpty(dto.Type) && dto.Type != "All")
			{
				query = query.Where(cl => cl.Type == dto.Type);
			}

			if (!string.IsNullOrEmpty(dto.Brand) && dto.Brand != "All")
			{
				query = query.Where(cl => cl.Brand == dto.Brand);
			}

			if (!string.IsNullOrEmpty(dto.PriceRange) && dto.PriceRange != "All")
			{
				switch (dto.PriceRange)
				{
					case "0-10000":
						query = query.Where(cl => cl.Price <= 10000);
						break;
					case "10000-30000":
						query = query.Where(cl => cl.Price > 10000 && cl.Price <= 30000);
						break;
					case "50000+":
						query = query.Where(cl => cl.Price >= 50000);
						break;
				}
			}

			var paginatedQuery = await repository.GetPaginatedAsync(pageIndex, pageSize, query);
			var totalPages = paginatedQuery.TotalPages;

			var items = paginatedQuery.Select(cl => new CarListingListDetailsDto
			{
				Id = cl.Id,
				Name = cl.Name,
				Price = cl.Price,
				ImageURL = cl.ImageURL,
			}).ToList();

			return new CarListingPaginatedSearchDto
			{
				Items = items,
				TotalPages = totalPages,
			};
		}

		public async Task<CarListingPaginatedSearchDto> GetPaginatedCarListingsAsync(int? pageIndex, int pageSize)
		{
			var listings = await repository.GetPaginatedAsync(pageIndex, pageSize);
			int totalPages = listings.TotalPages;

			var items = listings.Select(cl => new CarListingListDetailsDto
			{
				Id = cl.Id,
				Name = cl.Name,
				Price = cl.Price,
				ImageURL = cl.ImageURL,
			}).ToList();

			return new CarListingPaginatedSearchDto
			{
				Items = items,
				TotalPages = totalPages,
			};
		}
	}
}