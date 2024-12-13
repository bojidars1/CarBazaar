using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories
{
	public class FavouriteCarListingRepository(CarBazaarDbContext context) : Repository<FavouriteCarListing>(context), IFavouriteCarListingRepository
	{
	}
}