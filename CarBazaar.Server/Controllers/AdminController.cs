using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Policy = "RequireAdministratorRole")]
	public class AdminController : BaseController
	{
		[HttpGet("get-users")]
		public async Task<IActionResult> GetUsers()
		{

		}
	}
}