using CarBazaar.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class ChatService : IChatService
	{
		public bool IsOneOfThemOwner(string userId, string receiverId, string carListingId)
		{
			throw new NotImplementedException();
		}

		public Task SendMessageAsync(string userId, string receiverId, string carListingId, string message)
		{
			throw new NotImplementedException();
		}
	}
}