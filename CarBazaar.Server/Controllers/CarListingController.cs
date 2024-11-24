using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.CarListing;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CarListingController(CarBazaarDbContext context) : Controller
	{
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
				PublicationDate = DateTime.Now
			};

			context.CarListings.Add(carListing);
			await context.SaveChangesAsync();

			return Ok(carListing);
		}
	}
}