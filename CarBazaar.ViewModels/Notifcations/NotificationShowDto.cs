using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Notifcations
{
	public class NotificationShowDto
	{
		public string Id { get; set; } = null!;

		public string SenderId { get; set; } = null!;

		public string CarListingId { get; set; } = null!;

		public string Message { get; set; } = null!;

		public bool IsRead { get; set; } = false;
	}
}