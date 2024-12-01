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
			var listings = await service.GetAllAsync();
			return Ok(listings);
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