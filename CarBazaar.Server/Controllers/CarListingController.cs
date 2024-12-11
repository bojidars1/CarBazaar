using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	public class CarListingController(ICarListingService service) : BaseController
	{
		[HttpGet]
		[Authorize(AuthenticationSchemes = "Bearer")]
		[ProducesResponseType<CarListingPaginatedSearchDto>(200)]
		public async Task<IActionResult> GetListings([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var listings = await service.GetPaginatedCarListingsAsync(page, pageSize);
			return Ok(listings);
		}

		[HttpPost]
		[Authorize]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> Add([FromBody] AddCarListingDto dto)
		{
			var userId = "";

			if (userId == null)
			{
				return Unauthorized("Please login or create an account to add a Car Listing");
			}

			await service.AddAsync(dto, userId);

			return Ok("Success");
		}

		[HttpGet("{id}")]
		[ProducesResponseType<CarListingDetailsDto>(200)]
		[ProducesResponseType(404)]
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
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Edit([FromBody] EditCarListingDto dto)
		{
			bool isUpdated = await service.UpdateCarListingAsync(dto);

			if (!isUpdated)
			{
				return NotFound();
			}

			return Ok("Success");
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
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
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
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
		[ProducesResponseType<CarListingPaginatedSearchDto>(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Search([FromQuery] CarListingSearchDto dto, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var results = await service.SearchCarListingsAsync(dto, page, pageSize);
			return Ok(results);
		}
	}
}