using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected string? GetUserId()
		{
			string? userId = User.FindFirst("sub")?.Value;
			return userId;
		}
	}
}