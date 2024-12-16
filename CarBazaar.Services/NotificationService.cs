using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Notifcations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class NotificationService(INotificationRepository notificationRepository) : INotificationService
	{
		public async Task<NotificationShowPaginatedDto> GetNotificationsAsync(string userId, int page, int pageSize)
		{

		}
	}
}