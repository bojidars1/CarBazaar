using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories
{
	public class UserCarListingRepository(CarBazaarDbContext context) : Repository<UserCarListing>(context), IUserCarListingRepository
	{
		public async Task<UserCarListing?> GetByCarIdAsync(string carId)
		{
			UserCarListing? listing = await context.UserCarListings
				.FirstOrDefaultAsync(cl => cl.CarListingId.ToString() == carId);

			return listing;
		}

		public async Task<UserCarListing?> GetByUserIdAsync(string userId)
		{
			UserCarListing? listing = await context.UserCarListings
				.FirstOrDefaultAsync(cl => cl.UserId == userId);
			return listing;
		}
	}
}