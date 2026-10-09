using DataAccess.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber = 1, int pageSize = 10);
        Task<int> GetUsersCountAsync();


        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetUserByIdAsync(int Id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetUserWithRolesAsync(int userId);
        Task<User> GetUserWithPermissionsAsync(int userId);
        Task<User> GetUserWithSubscriptionAsync(int userId);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
      //  Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId);
        Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(int userId);
        Task<IEnumerable<Permission>> GetUserEffectivePermissionsAsync(int userId);


        Task AssignRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
        Task<IEnumerable<Role>> GetUserRolesAsync(int userId);

        // Permission management methods
        Task AssignPermissionToUserAsync(int userId, int permissionId, DateTime? expiresAt = null);
        Task RemovePermissionFromUserAsync(int userId, int permissionId);
        Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId, bool includeExpired = false);
        Task<IEnumerable<Permission>> GetAllUserPermissionsAsync(int userId, bool includeExpired = false);
        Task<bool> UserHasPermissionAsync(int userId, string permissionName);
    }
}
