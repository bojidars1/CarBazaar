using CarBazaar.Data.Models;
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
    }
}