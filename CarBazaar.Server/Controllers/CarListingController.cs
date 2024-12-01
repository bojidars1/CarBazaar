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
		public async Task<IActionResult> Add([FromBody] AddCarListingDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await service.AddAsync(dto);

			return Ok("Success");
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Details(string id)
		{
			if (id == string.Empty || id == null)
			{
				return NotFound();
			}

			CarListingDetailsDto? listing = await service.GetCarListingDetailsByIdAsync(id);
			if (listing == null)
			{
				return NotFound();
			}

			return Ok(listing);
		}

		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditCarListingDto dto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			bool isUpdated = await service.UpdateCarListingAsync(dto);

			if (!isUpdated)
			{
				return NotFound();
			}

			return Ok("Success");
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> SoftDelete(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest();
			}

			bool isDeleted = await service.SoftDeleteCarListingAsync(id);
			if (!isDeleted)
			{
				return NotFound();
			}

			return Ok("Success");
		}

		[HttpGet("delete/{id}")]
		public async Task<IActionResult> GetSoftDeleteDto(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return BadRequest();
			}

			DeleteCarListingDto? listing = await service.GetDeleteCarListingDtoByIdAsync(id);

			if (listing == null)
			{
				return NotFound();
			}

			return Ok(listing);
		}

		[HttpGet("search")]
		public async Task<IActionResult> Search([FromQuery] CarListingSearchDto dto)
		{
			var results = await service.SearchCarListingsAsync(dto);
			return Ok(results);
		}
	}
}