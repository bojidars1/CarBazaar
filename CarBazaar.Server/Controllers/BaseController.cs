using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected string? GetUserId()
		{
			var token = Request.Headers.Authorization.ToString().Replace("Bearer ", "");

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);
			var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

			return userIdClaim;
		}
	}
}