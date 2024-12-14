﻿using CarBazaar.Data;
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
	public class ChatController(IChatService chatService) : BaseController
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

			await chatService.SendMessageAsync(userId, request.ReceiverId, request.CarListingId, request.Message);

			return Ok("Message sent");
		}

		[HttpGet("messages/{carId}/{participantId}")]
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

		[HttpGet("summaries")]
		public async Task<IActionResult> GetChatSummaries()
		{
			var userId = GetUserId();
			if (userId == null)
			{
				return BadRequest("User id not found");
			}

			var chatSummaries = await context.ChatMessages.Where(cm => cm.SenderId == userId || cm.ReceiverId == userId)
				.GroupBy(cm => new { cm.CarListingId, OtherParticipant = cm.SenderId == userId ? cm.ReceiverId : cm.SenderId })
				.Select(g => new
				{
					CarListingId = g.Key.CarListingId,
					OtherParticipantId = g.Key.OtherParticipant,
					LastMessage = g.OrderByDescending(cm => cm.Timestamp).FirstOrDefault()
				})
				.ToListAsync();

			var result = chatSummaries.Select(async cs => new ChatSummaryDto
			{
				CarListingId = cs.CarListingId,
				OtherParticipantId = cs.OtherParticipantId,
				OtherParticipantName = await context.Users
				.Where(u => u.Id == cs.OtherParticipantId)
				.Select(u => u.Email)
				.FirstOrDefaultAsync(),
				LastMessage = cs.LastMessage.Message,
				LastMessageTimestamp = cs.LastMessage.Timestamp

			}).ToList();

			return Ok(await Task.WhenAll(result));
		}
	}
}