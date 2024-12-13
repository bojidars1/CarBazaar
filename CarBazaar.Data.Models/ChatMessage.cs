using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Data.Models
{
	public class ChatMessage
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Comment("Sender Id")]
		public string SenderId { get; set; } = null!;

		[ForeignKey(nameof(SenderId))]
		[Comment("Sender")]
		public CarBazaarUser Sender { get; set; } = null!;

		[Comment("Receiver Id")]
		public string ReceiverId { get; set; } = null!;

		[ForeignKey(nameof(ReceiverId))]
		[Comment("Receiver")]
		public CarBazaarUser Receiver { get; set; } = null!;

		[Comment("Message")]
		public string Message { get; set; } = null!;

		[Comment("Timestamp")]
		public DateTime Timestamp { get; set; }

		[Comment("Car Listing Id")]
		public Guid CarListingId { get; set; }

		[ForeignKey(nameof(CarListingId))]
		[Comment("Car Listing")]
		public CarListing CarListing { get; set; } = null!;
	}
}