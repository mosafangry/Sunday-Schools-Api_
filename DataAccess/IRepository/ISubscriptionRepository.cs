using DataAccess.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ISubscriptionRepository : IGenericRepository<Subscription>
    {
        Task<Subscription> GetSubscriptionWithPermissionsAsync(int subscriptionId);
        Task<IEnumerable<Permission>> GetSubscriptionPermissionsAsync(int subscriptionId);
        Task<UserSubscription> GetActiveUserSubscriptionAsync(int userId);
        Task<UserSubscription> CreateUserSubscriptionAsync(int userId, int subscriptionId, string transactionId);
    }
}
