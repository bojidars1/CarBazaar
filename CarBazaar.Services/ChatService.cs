using Azure.Core;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class ChatService(IUserCarListingRepository userCarListingRepository, IChatRepository chatRepository) : IChatService
	{
		public async Task<List<ChatSummaryDto>> GetChatSummariesAsync(string userId)
		{
			var allChatMessages = await chatRepository.GetAllAsync();

			var chatSummaries = allChatMessages
				.Where(cm => cm.SenderId == userId || cm.ReceiverId == userId)
				.GroupBy(cm => new { cm.CarListingId, OtherParticipant = cm.SenderId == userId ? cm.ReceiverId : cm.SenderId })
				.Select(g => new
				{
					CarListingId = g.Key.CarListingId,
					OtherParticipantId = g.Key.OtherParticipant,
					LastMessage = g.OrderByDescending(cm => cm.Timestamp).FirstOrDefault()
				})
				.ToList();

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
		}

		public async Task<List<MessageDto>> GetMessagesAsync(string carListingId, string userId, string participantId)
		{
			var messages = await chatRepository.GetAllAsync();

			return messages
				.Where(cm => cm.CarListingId.ToString() == carListingId &&
				((cm.SenderId == userId && cm.ReceiverId == participantId) ||
				(cm.SenderId == participantId && cm.ReceiverId == userId)))
				.OrderBy(cm => cm.Timestamp)
				.Select(cm => new MessageDto
				{
					Message = cm.Message,
					SenderId = userId,
					ParticipantId = participantId,
				}).ToList();
		}

		public async Task<bool> IsOneOfThemOwner(string userId, string receiverId, string carListingId)
		{
			var userCarListings = await userCarListingRepository.GetAllAsync();
			bool isOneOfThemOwner = userCarListings.Any(ucl => ucl.CarListingId.ToString() == carListingId &&
			((ucl.UserId == userId) || (ucl.UserId == receiverId)));

			return isOneOfThemOwner;
		}

		public async Task SendMessageAsync(string userId, string receiverId, Guid carListingId, string message)
		{
			var chatMessage = new ChatMessage
			{
				SenderId = userId,
				ReceiverId = receiverId,
				Message = message,
				CarListingId = carListingId,
				Timestamp = DateTime.UtcNow
			};

			await chatRepository.AddAsync(chatMessage);
		}
	}
}