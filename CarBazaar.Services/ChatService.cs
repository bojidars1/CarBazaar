using Azure.Core;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class ChatService(IUserCarListingRepository userCarListingRepository, IChatRepository chatRepository) : IChatService
	{
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