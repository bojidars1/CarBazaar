using CarBazaar.ViewModels.Notifcations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Services.Contracts
{
    public interface INotificationService
    {
        Task<NotificationShowPaginatedDto> GetNotificationsAsync(string userId, int page, int pageSize);
    }
}