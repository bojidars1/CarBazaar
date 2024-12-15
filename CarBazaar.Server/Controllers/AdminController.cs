using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Policy = "RequireAdministratorRole")]
	public class AdminController(IUserService userService) : BaseController
	{
		[HttpGet("get-users")]
		public async Task<IActionResult> GetUsersPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
		{
			var users = await userService.GetUserInfoPaginated(page, pageSize);
			return Ok(users);
		}
	}
}