using CarBazaar.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	public class AccountController(UserManager<CarBazaarUser> userManager, SignInManager<CarBazaarUser> signInManager) : BaseController
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register()
		{

		}
	}
}