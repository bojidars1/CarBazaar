using CarBazaar.Data.Models;
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
        Task MarkAsReadAsync(List<Guid> notificationsIds);
    }
}