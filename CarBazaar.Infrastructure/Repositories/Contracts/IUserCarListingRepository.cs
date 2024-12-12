using CarBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Contracts
{
    public interface IUserCarListingRepository : IRepository<UserCarListing>
    {
        Task<UserCarListing?> GetByCarIdAsync(string carId);
    }
}