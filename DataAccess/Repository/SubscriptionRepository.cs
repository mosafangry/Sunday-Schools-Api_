using DataAccess.DatabaseContext;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(PharmaDbContext context, ILogger<SubscriptionRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<Subscription> GetSubscriptionWithPermissionsAsync(int subscriptionId)
        {
            try
            {
                return await _dbSet
                    .Include(s => s.SubscriptionPermissions)
                    .ThenInclude(sp => sp.Permission)
                    .FirstOrDefaultAsync(s => s.SubscriptionId == subscriptionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription with permissions for subscriptionId {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetSubscriptionPermissionsAsync(int subscriptionId)
        {
            try
            {
                return await _context.SubscriptionPermissions
                    .Where(sp => sp.SubscriptionId == subscriptionId)
                    .Include(sp => sp.Permission)
                    .Select(sp => sp.Permission)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription permissions for subscriptionId {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task<UserSubscription> GetActiveUserSubscriptionAsync(int userId)
        {
            try
            {
                return await _context.UserSubscriptions
                    .Include(us => us.Subscription)
                    .FirstOrDefaultAsync(us => us.UserId == userId &&
                                             us.IsActive &&
                                             us.IsPaid &&
                                             us.EndDate > DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active user subscription for userId {UserId}", userId);
                throw;
            }
        }

        public async Task<UserSubscription> CreateUserSubscriptionAsync(int userId, int subscriptionId, string transactionId)
        {
            try
            {
                var subscription = await GetByIdAsync(subscriptionId);
                if (subscription == null)
                    throw new ArgumentException("Invalid subscription ID");

                var userSubscription = new UserSubscription
                {
                    UserId = userId,
                    SubscriptionId = subscriptionId,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(subscription.DurationInDays),
                    IsActive = true,
                    IsPaid = true,
                    PaymentTransactionId = transactionId
                };

                await _context.UserSubscriptions.AddAsync(userSubscription);
                return userSubscription;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user subscription for userId {UserId}, subscriptionId {SubscriptionId}", userId, subscriptionId);
                throw;
            }
        }
    }
}
