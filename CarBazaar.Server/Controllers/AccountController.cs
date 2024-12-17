using CarBazaar.Data.Models;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	public class AccountController(
		IJwtService jwtService,
		IRedisService redisService,
		UserManager<CarBazaarUser> userManager) : BaseController
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
				await userManager.AddToRoleAsync(user, "User");
				var accessToken = await jwtService.GenerateAccessToken(user.Id, user.Email);
				return Ok(new { accessToken });
			}

			return BadRequest(ModelState);
		}

		[HttpPost("login")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> Login([FromBody] LoginDto dto)
		{
			var user = await userManager.FindByEmailAsync(dto.Email);
			if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
			{
				return BadRequest("Invalid username or password");
			}

			var accessToken = await jwtService.GenerateAccessToken(user.Id, dto.Email);
			return Ok(new { accessToken });
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			var token = Request.Headers.Authorization.ToString()?.Replace("Bearer ", "");
			if (string.IsNullOrEmpty(token))
			{
				return BadRequest("No access token provided.");
			}

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);

			var expiry = jwtToken.ValidTo - DateTime.UtcNow;
			await redisService.AddToBlacklistAsync(token, expiry);

			return Ok("Logged out successfully");
		}
	}
}