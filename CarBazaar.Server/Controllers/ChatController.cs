using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ChatController(CarBazaarDbContext context) : BaseController
	{
		public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			var message = new ChatMessage
			{
				SenderId = userId,
				ReceiverId = request.ReceiverId,
				Message = request.Message,
				CarListingId = request.CarListingId,
				Timestamp = DateTime.UtcNow
			};

			await context.ChatMessages.AddAsync(message);
			await context.SaveChangesAsync();

			return Ok("Message sent");
		}
	}
}