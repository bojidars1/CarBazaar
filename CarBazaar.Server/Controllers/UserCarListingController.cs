using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarBazaar.Server.Controllers
{
	[Route("/api/[controller]")]
	[Authorize]
	public class UserCarListingController(IUserCarListingService userCarListingService) : BaseController
	{
		[HttpGet("get-listings")]
		public async Task<IActionResult> GetListings()
		{
			string? userId = GetUserId();
			if (string.IsNullOrEmpty(userId))
			{;
				return Unauthorized("Unauthorized");
			}

			var listings = await userCarListingService.GetListingsAsync(userId);
			return Ok(listings);
		}
	} 
}