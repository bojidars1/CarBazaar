﻿using CarBazaar.Data.Models;
using CarBazaar.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Contracts
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<PaginatedList<Notification>> GetUserNotificationsPaginated(string userId, int page, int pageSize);

        Task MarkAsReadAsync(List<string> notificationsIds);
    }
}