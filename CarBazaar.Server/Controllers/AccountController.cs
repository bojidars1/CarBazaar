using CarBazaar.Data.Models;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	public class AccountController(IJwtService jwtService, UserManager<CarBazaarUser> userManager) : BaseController
	{
		[HttpPost("register")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Register([FromBody] RegisterDto dto)
		{
			var user = new CarBazaarUser { UserName = dto.Email, Email = dto.Email };

			var result = await userManager.CreateAsync(user, dto.Password);
			if (result.Succeeded)
			{
				var token = jwtService.GenerateToken(user.Id, user.Email);
				return Ok(new { token });
			}

			return BadRequest(ModelState);
		}

		[HttpPost("login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var user = await userManager.FindByEmailAsync(dto.Email);
			if (user != null && await userManager.CheckPasswordAsync(user, dto.Password))
			{
				var token = jwtService.GenerateToken(user.Id, user.Email);
				return Ok(new { token });
			}

			// I won't let them know that such an account exists
			return BadRequest(ModelState);
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout([FromServices] IRedisService redisService)
		{
			var token = Request.Headers.Authorization.ToString()?.Replace("Bearer ", "");
			if (string.IsNullOrEmpty(token))
			{
				return BadRequest("No token provided.");
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			var expiry = jwtToken.ValidTo - DateTime.UtcNow;
			await redisService.AddToBlacklistAsync(token, expiry);

			return Ok("Logged out successfully");
		}
	}
}