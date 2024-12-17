using CarBazaar.Data.Models;
using CarBazaar.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class JwtService(IConfiguration config, IUserCarListingService userCarListingService,
		UserManager<CarBazaarUser> userManager) : IJwtService
	{
		public async Task<string> GenerateAccessToken(string userId, string email)
		{
			var user = await userManager.FindByIdAsync(userId);
			var roles = await userManager.GetRolesAsync(user);

			List<string> ids = new List<string>();

			var listings = await userCarListingService.GetListingsAsync(userId);
			if (listings != null)
			{
				foreach (var listing in listings.Items)
				{
					ids.Add(listing.Id.ToString());
				}
			}

			var idsToJson = System.Text.Json.JsonSerializer.Serialize(ids);

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim("CarListings", idsToJson)
			};

			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var issuer = config["JWT:Issuer"];
			var audience = config["JWT:Audience"];

			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(60),
				signingCredentials: creds
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}