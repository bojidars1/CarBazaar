using CarBazaar.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class JwtService(IConfiguration config) : IJwtService
	{
		public string GenerateToken(string userId, string email)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId),
				new Claim(JwtRegisteredClaimNames.Email, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

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