using CarBazaar.Data.Models;
using CarBazaar.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
    public interface IUserCarListingService
    {
        public Task AddAsync(UserCarListing listing);

        public Task<CarListingPaginatedSearchDto?> GetListingsAsync(string userId, int pageIndex = 1, int pageSize = 10);
    }
}