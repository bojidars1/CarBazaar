using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;

namespace CarBazaar.Server.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		public async Task SendMessage(string carListingId, string participantId, string message)
		{
			await Clients.User(participantId).SendAsync("ReceiveMessage", new {
				SenderId = Context.UserIdentifier,
				CarListingId = carListingId,
				Message = message
			});
		}
	}
}