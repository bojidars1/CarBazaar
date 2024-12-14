using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ChatController(CarBazaarDbContext context) : BaseController
	{
		[HttpPost("chat/send")]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			bool isOneOfThemOwner = await context.UserCarListings.AnyAsync(ucl => ucl.CarListingId == request.CarListingId && 
			((ucl.UserId == userId) || (ucl.UserId == request.ReceiverId)));

			if (!isOneOfThemOwner)
			{
				return BadRequest("One of users must be the owner of the car");
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

		[HttpGet("chat/messages/{carId}/{participantId}")]
		public async Task<IActionResult> GetChatMessages(string carId, string participantId)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			var messages = await context.ChatMessages
				.Where(cm => cm.CarListingId.ToString() == carId &&
				((cm.SenderId == userId && cm.ReceiverId == participantId) ||
				(cm.SenderId == participantId && cm.ReceiverId == userId)))
				.OrderBy(cm => cm.Timestamp)
				.ToListAsync();

			return Ok(messages);
		}
	}
}