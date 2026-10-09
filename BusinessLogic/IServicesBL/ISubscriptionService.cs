using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.Subscription;
using BusinessLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetSubscriptionByIdAsync(int subscriptionId);
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createSubscriptionDto);
        Task<SubscriptionDto> UpdateSubscriptionAsync(int subscriptionId, UpdateSubscriptionDto updateSubscriptionDto);
        Task DeleteSubscriptionAsync(int subscriptionId);
        Task<UserSubscriptionDto> CreateUserSubscriptionAsync(int userId, int subscriptionId, string transactionId);
        Task<UserSubscriptionDto> GetActiveUserSubscriptionAsync(int userId);
        Task<IEnumerable<PermissionDto>> GetSubscriptionPermissionsAsync(int subscriptionId);
        Task AssignPermissionToSubscriptionAsync(int subscriptionId, int permissionId);
        Task RemovePermissionFromSubscriptionAsync(int subscriptionId, int permissionId);
    }
}
