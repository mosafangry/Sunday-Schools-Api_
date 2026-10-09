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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PharmaDbContext context, ILogger<UserRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<User>> GetPaginatedUsersAsync(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Include(u => u.UserPermissions).ThenInclude(up => up.Permission)
                    .Include(u => u.UserSubscription)
                    .Include(u => u.Company)
                    .OrderBy(u => u.UserId) // Optional sorting
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated users.");
                throw;
            }
        }
        public async Task<int> GetUsersCountAsync()
        {
            try
            {
                return await _context.Users.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total user count.");
                throw;
            }
        }

        public async Task<User?> GetUserByIdAsync(int Id)
        {
            try
            {
                return await _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Include(u => u.UserPermissions).ThenInclude(up => up.Permission)
                    .Include(u => u.UserSubscription)
                    .Include(u => u.Company).FirstOrDefaultAsync(u => u.UserId == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by Id {UserId}", Id);
                throw;
            }
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _dbSet.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Include(u => u.UserPermissions).ThenInclude(up => up.Permission)
                    .Include(u => u.UserSubscription)
                    .Include(u => u.Company).FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email {Email}", email);
                throw;
            }
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username {Username}", username);
                throw;
            }
        }

        public async Task<User> GetUserWithRolesAsync(int userId)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user with roles for userId {UserId}", userId);
                throw;
            }
        }

        public async Task<User> GetUserWithPermissionsAsync(int userId)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.UserPermissions)
                    .ThenInclude(up => up.Permission)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user with permissions for userId {UserId}", userId);
                throw;
            }
        }

        public async Task<User> GetUserWithSubscriptionAsync(int userId)
        {
            try
            {
                return await _dbSet
                    .Include(u => u.UserSubscription)
                    .ThenInclude(us => us.Subscription)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user with subscription for userId {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await GetByEmailAsync(email);
                if (user == null || !user.IsActive)
                    return false;

                // In real implementation, use proper password hashing (BCrypt, etc.)
                //return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user credentials for email {Email}", email);
                throw;
            }
        }

        //public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId)
        //{
        //    try
        //    {
        //        return await _context.UserPermissions
        //            .Where(up => up.UserId == userId && (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow))
        //            .Include(up => up.Permission)
        //            .Select(up => up.Permission)
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error getting user permissions for userId {UserId}", userId);
        //        throw;
        //    }
        //}
        public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync(int userId)
        {
            try
            {
                return await _context.UserPermissions
                    .Where(up => up.UserId == userId && (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow))
                    .Include(up => up.Permission)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user permissions for userId {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetUserEffectivePermissionsAsync(int userId)
        {
            try
            {
                var user = await _dbSet
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .Include(u => u.UserPermissions.Where(up => up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow))
                    .ThenInclude(up => up.Permission)
                    .Include(u => u.UserSubscription)
                    .ThenInclude(us => us.Subscription)
                    .ThenInclude(s => s.SubscriptionPermissions)
                    .ThenInclude(sp => sp.Permission)
                    .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null) return new List<Permission>();

                var permissions = new HashSet<Permission>();

                // Add role-based permissions
                foreach (var userRole in user.UserRoles)
                {
                    foreach (var rolePermission in userRole.Role.RolePermissions)
                    {
                        permissions.Add(rolePermission.Permission);
                    }
                }

                // Add direct user permissions
                foreach (var userPermission in user.UserPermissions)
                {
                    permissions.Add(userPermission.Permission);
                }

                // Add subscription-based permissions
                if (user.UserSubscription != null &&
                    user.UserSubscription.IsActive &&
                    user.UserSubscription.IsPaid &&
                    user.UserSubscription.EndDate > DateTime.UtcNow)
                {
                    foreach (var subscriptionPermission in user.UserSubscription.Subscription.SubscriptionPermissions)
                    {
                        permissions.Add(subscriptionPermission.Permission);
                    }
                }

                return permissions.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting effective user permissions for userId {UserId}", userId);
                throw;
            }
        }
        //
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            try
            {
                // Check if user exists
                var userExists = await _context.Users.AnyAsync(u => u.UserId == userId && u.IsActive);
                if (!userExists)
                {
                    throw new ArgumentException($"User with ID {userId} not found or inactive");
                }

                // Check if role exists
                var roleExists = await _context.Roles.AnyAsync(r => r.RoleId == roleId && r.IsActive);
                if (!roleExists)
                {
                    throw new ArgumentException($"Role with ID {roleId} not found or inactive");
                }

                // Check if user already has this role
                var existingUserRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (existingUserRole != null)
                {
                    _logger.LogWarning("User {UserId} already has role {RoleId}", userId, roleId);
                    return; // Role already assigned
                }

                // Create new UserRole relationship
                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId,
                    AssignedAt = DateTime.UtcNow
                };

                await _context.UserRoles.AddAsync(userRole);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully assigned role {RoleId} to user {UserId}", roleId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning role to user: {UserId}, {RoleId}", userId, roleId);
                throw;
            }
        }

        public async Task RemoveRoleFromUserAsync(int userId, int roleId)
        {
            try
            {
                // Find the UserRole relationship
                var userRole = await _context.UserRoles
                    .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (userRole == null)
                {
                    _logger.LogWarning("UserRole relationship not found for user {UserId} and role {RoleId}", userId, roleId);
                    return; // Relationship doesn't exist
                }

                // Remove the UserRole relationship
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully removed role {RoleId} from user {UserId}", roleId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing role from user: {UserId}, {RoleId}", userId, roleId);
                throw;
            }
        }

        public async Task AssignPermissionToUserAsync(int userId, int permissionId, DateTime? expiresAt = null)
        {
            try
            {
                // Check if user exists
                var userExists = await _context.Users.AnyAsync(u => u.UserId == userId && u.IsActive);
                if (!userExists)
                {
                    throw new ArgumentException($"User with ID {userId} not found or inactive");
                }

                // Check if permission exists
                var permissionExists = await _context.Permissions.AnyAsync(p => p.PermissionId == permissionId && p.IsActive);
                if (!permissionExists)
                {
                    throw new ArgumentException($"Permission with ID {permissionId} not found or inactive");
                }

                // Check if user already has this permission (and it's not expired)
                var existingUserPermission = await _context.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId
                        && (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow));

                if (existingUserPermission != null)
                {
                    // Update expiration date if provided
                    if (expiresAt.HasValue)
                    {
                        existingUserPermission.ExpiresAt = expiresAt;
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Updated expiration date for permission {PermissionId} for user {UserId}", permissionId, userId);
                    }
                    else
                    {
                        _logger.LogWarning("User {UserId} already has active permission {PermissionId}", userId, permissionId);
                    }
                    return;
                }

                // Create new UserPermission relationship
                var userPermission = new UserPermission
                {
                    UserId = userId,
                    PermissionId = permissionId,
                    GrantedAt = DateTime.UtcNow,
                    ExpiresAt = expiresAt
                };

                await _context.UserPermissions.AddAsync(userPermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully assigned permission {PermissionId} to user {UserId}", permissionId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning permission to user: {UserId}, {PermissionId}", userId, permissionId);
                throw;
            }
        }

        public async Task RemovePermissionFromUserAsync(int userId, int permissionId)
        {
            try
            {
                // Find the UserPermission relationship
                var userPermission = await _context.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);

                if (userPermission == null)
                {
                    _logger.LogWarning("UserPermission relationship not found for user {UserId} and permission {PermissionId}", userId, permissionId);
                    return; // Relationship doesn't exist
                }

                // Remove the UserPermission relationship
                _context.UserPermissions.Remove(userPermission);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully removed permission {PermissionId} from user {UserId}", permissionId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing permission from user: {UserId}, {PermissionId}", userId, permissionId);
                throw;
            }
        }

        // Additional helper methods you might find useful:

        public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
        {
            try
            {
                return await _context.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Include(ur => ur.Role)
                    .Where(ur => ur.Role.IsActive)
                    .Select(ur => ur.Role)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting roles for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetUserPermissionsAsync(int userId, bool includeExpired = false)
        {
            try
            {
                var query = _context.UserPermissions
                    .Where(up => up.UserId == userId)
                    .Include(up => up.Permission)
                    .Where(up => up.Permission.IsActive);

                if (!includeExpired)
                {
                    query = query.Where(up => up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow);
                }

                return await query
                    .Select(up => up.Permission)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting permissions for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetAllUserPermissionsAsync(int userId, bool includeExpired = false)
        {
            try
            {
                // Get direct permissions
                var directPermissions = _context.UserPermissions
                    .Where(up => up.UserId == userId)
                    .Include(up => up.Permission)
                    .Where(up => up.Permission.IsActive);

                if (!includeExpired)
                {
                    directPermissions = directPermissions.Where(up => up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow);
                }

                // Get permissions from roles
                var rolePermissions = _context.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Include(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .Where(ur => ur.Role.IsActive)
                    .SelectMany(ur => ur.Role.RolePermissions)
                    .Where(rp => rp.Permission.IsActive)
                    .Select(rp => rp.Permission);

                // Combine and remove duplicates
                var allPermissions = await directPermissions.Select(up => up.Permission)
                    .Union(rolePermissions)
                    .Distinct()
                    .ToListAsync();

                return allPermissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all permissions for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> UserHasPermissionAsync(int userId, string permissionName)
        {
            try
            {
                // Check direct permissions
                var hasDirectPermission = await _context.UserPermissions
                    .AnyAsync(up => up.UserId == userId
                        && up.Permission.Name == permissionName
                        && up.Permission.IsActive
                        && (up.ExpiresAt == null || up.ExpiresAt > DateTime.UtcNow));

                if (hasDirectPermission)
                    return true;

                // Check role-based permissions
                var hasRolePermission = await _context.UserRoles
                    .Where(ur => ur.UserId == userId)
                    .Include(ur => ur.Role)
                    .ThenInclude(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .AnyAsync(ur => ur.Role.IsActive
                        && ur.Role.RolePermissions.Any(rp => rp.Permission.Name == permissionName && rp.Permission.IsActive));

                return hasRolePermission;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking permission for user: {UserId}, Permission: {PermissionName}", userId, permissionName);
                throw;
            }
        }

    }
}
