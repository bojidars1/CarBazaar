using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Server.Hubs;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Server.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ChatController(IChatService chatService, INotificationService notificationService) : BaseController
	{
		[HttpPost("send")]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			bool isOneOfThemOwner = await chatService.IsOneOfThemOwner(userId, request.ReceiverId, request.CarListingId.ToString());
			if (!isOneOfThemOwner)
			{
				return BadRequest("One of users must be the owner of the car");
			}

			if (userId == request.ReceiverId)
			{
				return BadRequest("Can't message yourself");
			}

			var userEmail = GetUserEmail();

			await chatService.SendMessageAsync(userId, request.ReceiverId, request.CarListingId, request.Message);
			await notificationService.AddNotificationAsync(new Notification
			{
				UserId = request.ReceiverId,
				SenderId = userId,
				CarListingId = request.CarListingId,
				Message = $"You have a new message from {userEmail}"
			});

			return Ok("Message sent");
		}

		[HttpGet("messages/{carListingId}/{participantId}")]
		public async Task<IActionResult> GetChatMessages(string carListingId, string participantId)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			var messages = await chatService.GetMessagesAsync(carListingId, userId, participantId);

			return Ok(messages);
		}

		[HttpGet("summaries")]
		public async Task<IActionResult> GetChatSummaries([FromQuery] int page = 1, int pageSize = 10)
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			var chatSummaries = await chatService.GetChatSummariesAsync(userId, page, pageSize);

			return Ok(chatSummaries);
		}
	}
}