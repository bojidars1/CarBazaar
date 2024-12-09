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
				var token = jwtService.GenerateAccessToken(user.Id, user.Email);
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
				var accessToken = jwtService.GenerateAccessToken(user.Id, user.Email);
				var refreshToken = jwtService.GenerateRefreshToken();

				await redisService.StoreRefreshTokenAsync(user.Id, refreshToken, TimeSpan.FromDays(30));

				Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.Strict,
					Expires = DateTime.UtcNow.AddDays(30)
				});

				return Ok(new { accessToken });
			}

			// I won't let them know that such an account exists
			return BadRequest("Invalid email or password");
		}

		[HttpPost("logout")]
		[Authorize]
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