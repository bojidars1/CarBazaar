using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarBazaar.Server.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessageAsync(string receiverId, string carListingId, string message)
		{
			await Clients.User(receiverId).SendAsync("ReceiveMessage", new {
				SenderId = Context.UserIdentifier,
				CarListingId = carListingId,
				Message = message
			});
		}
	}
}