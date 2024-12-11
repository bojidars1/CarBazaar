using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("/api/[controller]")]
	[Authorize]
	public class UserCarListingController(IUserCarListingService userCarListingService) : BaseController
	{
		[HttpGet("get-listings")]
		public async Task<IActionResult> GetListings()
		{
			
			var listings = await userCarListingService.GetListingsAsync();
		}
	}
}