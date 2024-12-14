using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Chat
{
	public class MessageDto
	{
		public string Message { get; set; } = null!;

		public string SenderId { get; set; } = null!;

		public string ParticipantId { get; set; } = null!;

		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
	}
}