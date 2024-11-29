using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CarListingController(CarBazaarDbContext context) : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetListings()
		{
			var listings = await context.CarListings.AsNoTracking().ToListAsync();
			return Ok(listings.Select(l => new CarListingListDetailsDto
			{
				Id = l.Id,
				Name = l.Name,
				Price = l.Price,
				ImageURL = l.ImageURL
			}).ToList());
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody]AddCarListingDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

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

			context.CarListings.Add(carListing);
			await context.SaveChangesAsync();

			return Ok(carListing);
		}
	}
}