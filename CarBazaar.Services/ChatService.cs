using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class ChatService(IUserCarListingRepository userCarListingRepository) : IChatService
	{
		public async Task<bool> IsOneOfThemOwner(string userId, string receiverId, string carListingId)
		{
			var userCarListings = await userCarListingRepository.GetAllAsync();
			bool isOneOfThemOwner = userCarListings.Any(ucl => ucl.CarListingId.ToString() == carListingId &&
			((ucl.UserId == userId) || (ucl.UserId == receiverId)));

			return isOneOfThemOwner;
		}

		public Task SendMessageAsync(string userId, string receiverId, string carListingId, string message)
		{
			throw new NotImplementedException();
		}
	}
}