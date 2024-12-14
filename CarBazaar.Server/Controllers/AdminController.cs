using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize(Roles = "Administrator")]
	public class AdminController : BaseController
	{
		[HttpGet]
		public IActionResult TestAdminRole()
		{
			return Ok("You are an admin");
		}
	}
}