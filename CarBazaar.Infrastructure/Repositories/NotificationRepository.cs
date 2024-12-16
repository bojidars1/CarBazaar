using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories
{
	public class NotificationRepository(CarBazaarDbContext context) : Repository<Notification>(context), INotificationRepository
	{
		public async Task<List<Notification>> GetUserNotificationsByUserIdAsync(string userId)
		{
			var userNotifications = await GetAllAsync();
			return userNotifications.Where(n => n.UserId == userId).ToList();
		}
	}
}