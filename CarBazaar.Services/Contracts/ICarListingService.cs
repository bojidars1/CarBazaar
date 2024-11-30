using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
	public interface ICarListingService
	{
		public Task AddAsync([FromBody]AddCarListingDto dto);
		
		public Task<List<CarListingListDetailsDto>> GetAllAsync();
	}
}