using CarBazaar.Data;
using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
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
		public async Task<PaginatedList<Notification>> GetUserNotificationsPaginated(string userId, int page, int pageSize)
		{
			var query = GetBaseQuery();
			query.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt);

			return await GetPaginatedAsync(page, pageSize, query);
		}

		//public Task MarkAsReadAsync(List<string> notificationsIds)
		//{
		//	throw new NotImplementedException();
		//}
	}
}