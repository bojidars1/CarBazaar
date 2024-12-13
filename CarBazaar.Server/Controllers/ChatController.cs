using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ChatController : BaseController
	{
		
	}
}