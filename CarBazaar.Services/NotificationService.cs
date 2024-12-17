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

		public async Task<bool> DeleteAsync(string userId, string notificationId)
		{
			var notification = await notificationRepository.GetByIdAsync(notificationId);
			if (notification == null)
			{
				return false;
			}

			if (notification.UserId != userId)
			{
				return false;
			}

			await notificationRepository.DeleteAsync(notification);
			return true;
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
				SenderId = n.SenderId,
				CarListingId = n.CarListingId.ToString(),
				Message = n.Message,
				IsRead = n.IsRead
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