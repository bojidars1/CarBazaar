using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.ViewModels.Notifcations
{
	public class NotificationShowPaginatedDto
	{
		public List<NotificationShowDto> Items { get; set; } = new List<NotificationShowDto>();

		public int TotalPages { get; set; }
	}
}