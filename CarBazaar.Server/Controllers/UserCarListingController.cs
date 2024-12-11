﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("/api/[controller]")]
	[Authorize]
	public class UserCarListingController : BaseController
	{
		[HttpGet("get-listings")]
		public Task<IActionResult> GetListings()
		{

		}
	}
}