using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using CarBazaar.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories
{
	public class NotificationRepository(CarBazaarDbContext context) : Repository<Notification>(context), INotificationRepository
	{
		public async Task<PaginatedList<Notification>> GetUserNotificationsPaginated(string userId, int page, int pageSize)
		{
			var query = GetBaseQuery();
			query.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt);

			return await GetPaginatedAsync(page, pageSize, query);
		}

		public async Task MarkAsReadAsync(List<Guid> notificationsIds)
		{
			var notifications = await context.Notifications
				.Where(n => notificationsIds.Contains(n.Id))
				.ToListAsync();

			notifications.ForEach(n => n.isRead = true);
			await context.SaveChangesAsync();
		}
	}
}