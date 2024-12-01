﻿using CarBazaar.ViewModels.CarListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
	public interface ICarListingService
	{
		public Task AddAsync(AddCarListingDto dto);
		
		public Task<List<CarListingListDetailsDto>> GetAllAsync();

		public Task<CarListingDetailsDto?> GetCarListingDetailsByIdAsync(string id);

		public Task<bool> UpdateCarListingAsync(EditCarListingDto dto);
	}
}