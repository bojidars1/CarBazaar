using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
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
	}
}