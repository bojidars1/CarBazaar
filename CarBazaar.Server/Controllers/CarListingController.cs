using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CarListingController(ICarListingService service) : Controller
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

			await service.AddAsync(dto);

			return Ok("Success");
		}
	}
}