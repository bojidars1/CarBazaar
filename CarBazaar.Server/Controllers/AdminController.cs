using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.CarListing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Policy = "RequireAdministratorRole")]
	public class AdminController(IUserService userService,ICarListingService carListingService) : BaseController
	{
		[HttpGet("get-users")]
		public async Task<IActionResult> GetUsersPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var users = await userService.GetUserInfoPaginated(page, pageSize);
			return Ok(users);
		}

		[HttpGet("get-listings")]
		public async Task<IActionResult> GetListingsPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var listings = await carListingService.GetPaginatedCarListingsAsync(page, pageSize);
			return Ok(listings);
		}
	}
}