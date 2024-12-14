using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CarBazaar.Server.Hubs
{
	public class ChatHub : Hub
	{
		public async Task SendMessageAsync(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}