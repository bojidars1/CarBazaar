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
        Task AddAsync(UserCarListing listing);

        Task<CarListingPaginatedSearchDto?> GetListingsAsync(string userId, int pageIndex = 1, int pageSize = 10);

        Task<UserCarListing?> GetByCarIdAsync(string carId);

        Task<UserCarListing?> GetByUserIdAsync(string userId);
    }
}