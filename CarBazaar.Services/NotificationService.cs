using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Repositories.Contracts;
using CarBazaar.Services.Contracts;
using CarBazaar.ViewModels.Notifcations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services
{
	public class NotificationService(INotificationRepository notificationRepository) : INotificationService
	{
		public async Task AddNotificationAsync(Notification notification)
		{
			await notificationRepository.AddAsync(notification);
		}

		public async Task<NotificationShowPaginatedDto> GetNotificationsAsync(string userId, int page, int pageSize)
		{
			var query = notificationRepository.GetBaseQuery();
			query = query.Where(n => n.UserId == userId).OrderByDescending(n => n.CreatedAt);

			var userNotificationsPaginated = await notificationRepository.GetPaginatedAsync(page, pageSize, query);

			int totalPages = userNotificationsPaginated.TotalPages;
			var items = userNotificationsPaginated.Select(n => new NotificationShowDto
			{
				Id = n.Id.ToString(),
				Message = n.Message,
				isRead = n.isRead
			}).ToList();

			return new NotificationShowPaginatedDto
			{
				Items = items,
				TotalPages = totalPages
			};
		}

		public async Task MarkNotificationsAsReadAsync(List<Guid> notificationsIds)
		{
			await notificationRepository.MarkAsReadAsync(notificationsIds);
		}
	}
}