﻿using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class CarListingService(ICarListingRepository repository) : ICarListingService
	{
		public async Task AddAsync([FromBody] AddCarListingDto dto)
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

		public async Task<CarListingDetailsDto?> GetCarListingDetailsByIdAsync([FromQuery] string id)
		{
			var listing = await repository.GetByIdAsync(id);

			if (listing == null)
			{
				return null;
			}

			return new CarListingDetailsDto
			{
				Id = listing.Id,
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
	}
}