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
	public class Notification
	{
		[Key]
		[Comment("Notification identifier")]
		public Guid Id { get; set; } = Guid.NewGuid();

		[Comment("User Id")]
		public string UserId { get; set; } = null!;

		[ForeignKey(nameof(UserId))]
		[Comment("The user that receives the notification")]
		public CarBazaarUser User { get; set; } = null!;

		[Comment("Car Listing Id")]
		public Guid CarListingId { get; set; }

		[ForeignKey(nameof(CarListingId))]
		[Comment("The user that receives the notification")]
		public CarListing CarListing { get; set; } = null!;

		[Comment("The Notificaiton Message")]
		public string Message { get; set; } = null!;

		public bool isRead { get; set; } = false;

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}