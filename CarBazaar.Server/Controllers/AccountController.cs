using CarBazaar.Data.Models;
using CarBazaar.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	public class AccountController(UserManager<CarBazaarUser> userManager, SignInManager<CarBazaarUser> signInManager) : BaseController
	{
		[HttpPost("register")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			if (dto.Password != dto.ConfirmPassword)
			{
				return BadRequest(new { message = "Passwords does not match!"});
			}

			var user = new CarBazaarUser { UserName = dto.Email, Email = dto.Email };

			var result = await userManager.CreateAsync(user, dto.Password);
			if (result.Succeeded)
			{
				return Ok(new { message = "Registration successful" });
			}

			return BadRequest(ModelState);
		}

		[HttpPost("login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var result = await signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
			if (result.Succeeded)
			{
				return Ok(new { message = "Login successful" });
			}

			return Unauthorized(new { message = "Invalid email or password" });
		}
	}
}