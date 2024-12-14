using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Chat
{
	public class SendMessageRequest
	{
		[Required]
		public string ReceiverId { get; set; } = null!;

		[Required]
		[MaxLength(1000)]
		public string Message { get; set; } = string.Empty;

		[Required]
		public Guid CarListingId { get; set; } 
	}
}