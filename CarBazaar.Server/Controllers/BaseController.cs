using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected string GetUserId()
		{
			string userId = string.Empty;

			if (User != null)
			{
				userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			}

			return userId;
		}
	}
}