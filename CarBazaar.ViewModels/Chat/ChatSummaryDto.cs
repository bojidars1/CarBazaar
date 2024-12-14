using CarBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Chat
{
	public class ChatSummaryDto
	{
		public Guid CarListingId { get; set; }

		public string OtherParticipantId { get; set; } = null!;

		public string OtherParticipantName { get; set;} = null!;

		public ChatMessage LastMessage { get; set; } = null!;

		public DateTime LastMessageTimeStamp { get; set; }
	}
}