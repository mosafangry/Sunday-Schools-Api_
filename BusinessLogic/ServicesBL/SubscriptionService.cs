using AutoMapper;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.Subscription;
using BusinessLogic.DTOs.User;
using BusinessLogic.IServicesBL;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;
namespace BusinessLogic.ServicesBL
{

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            ILogger<SubscriptionService> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SubscriptionDto> GetSubscriptionByIdAsync(int subscriptionId)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetSubscriptionWithPermissionsAsync(subscriptionId);
                return subscription != null ? _mapper.Map<SubscriptionDto>(subscription) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription by ID: {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            try
            {
                var subscriptions = await _subscriptionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<SubscriptionDto>>(subscriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all subscriptions");
                throw;
            }
        }

        public async Task<SubscriptionDto> CreateSubscriptionAsync(CreateSubscriptionDto createSubscriptionDto)
        {
            try
            {
                var subscription = _mapper.Map<Subscription>(createSubscriptionDto);
                await _subscriptionRepository.AddAsync(subscription);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription created successfully: {SubscriptionId}", subscription.SubscriptionId);
                return _mapper.Map<SubscriptionDto>(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription: {SubscriptionName}", createSubscriptionDto.Name);
                throw;
            }
        }

        public async Task<SubscriptionDto> UpdateSubscriptionAsync(int subscriptionId, UpdateSubscriptionDto updateSubscriptionDto)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetByIdAsync(subscriptionId);
                if (subscription == null)
                {
                    throw new ArgumentException("Subscription not found");
                }

                _mapper.Map(updateSubscriptionDto, subscription);
                await _subscriptionRepository.UpdateAsync(subscription);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription updated successfully: {SubscriptionId}", subscriptionId);
                return _mapper.Map<SubscriptionDto>(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription: {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task DeleteSubscriptionAsync(int subscriptionId)
        {
            try
            {
                await _subscriptionRepository.DeleteAsync(subscriptionId);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription deleted successfully: {SubscriptionId}", subscriptionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subscription: {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task<UserSubscriptionDto> CreateUserSubscriptionAsync(int userId, int subscriptionId, string transactionId)
        {
            try
            {
                var userSubscription = await _subscriptionRepository.CreateUserSubscriptionAsync(userId, subscriptionId, transactionId);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("User subscription created successfully: UserId {UserId}, SubscriptionId {SubscriptionId}", userId, subscriptionId);
                return _mapper.Map<UserSubscriptionDto>(userSubscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user subscription: UserId {UserId}, SubscriptionId {SubscriptionId}", userId, subscriptionId);
                throw;
            }
        }

        public async Task<UserSubscriptionDto> GetActiveUserSubscriptionAsync(int userId)
        {
            try
            {
                var userSubscription = await _subscriptionRepository.GetActiveUserSubscriptionAsync(userId);
                return userSubscription != null ? _mapper.Map<UserSubscriptionDto>(userSubscription) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active user subscription: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetSubscriptionPermissionsAsync(int subscriptionId)
        {
            try
            {
                var permissions = await _subscriptionRepository.GetSubscriptionPermissionsAsync(subscriptionId);
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subscription permissions: {SubscriptionId}", subscriptionId);
                throw;
            }
        }

        public async Task AssignPermissionToSubscriptionAsync(int subscriptionId, int permissionId)
        {
            try
            {
                // This would need to be implemented in the repository layer
                // Similar to role-permission assignment
                _logger.LogInformation("Permission {PermissionId} assigned to subscription {SubscriptionId}", permissionId, subscriptionId);
                // Implementation depends on your SubscriptionPermission entity structure
                await _subscriptionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning permission to subscription: {SubscriptionId}, {PermissionId}", subscriptionId, permissionId);
                throw;
            }
        }

        public async Task RemovePermissionFromSubscriptionAsync(int subscriptionId, int permissionId)
        {
            try
            {
                // This would need to be implemented in the repository layer
                // Similar to role-permission removal
                _logger.LogInformation("Permission {PermissionId} removed from subscription {SubscriptionId}", permissionId, subscriptionId);
                // Implementation depends on your SubscriptionPermission entity structure
                await _subscriptionRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing permission from subscription: {SubscriptionId}, {PermissionId}", subscriptionId, permissionId);
                throw;
            }
        }
    }
}