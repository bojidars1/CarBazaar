using CarBazaar.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarBazaar.Infrastructure.Repositories.Contracts
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetUserNotificationsByUserIdAsync(string userId);
    }
}