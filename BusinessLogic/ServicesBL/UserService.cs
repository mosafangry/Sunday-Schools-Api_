using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.User;
using BusinessLogic.IServicesBL;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using DataAccess.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null) return null;
                //user.UserPermissions = (await _userRepository.GetUserPermissionsAsync(userId)).ToList();
                //user.UserSubscription = (await _userRepository.GetUserWithSubscriptionAsync(userId))?.UserSubscription;

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(email);
                return user != null ? _mapper.Map<UserDto>(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by email: {Email}", email);
                throw;
            }
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(username);
                return user != null ? _mapper.Map<UserDto>(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user by username: {Username}", username);
                throw;
            }
        }

        public async Task<PaginatedResponse<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var usersTask = await _userRepository.GetPaginatedUsersAsync(pageNumber, pageSize);
                var totalCountTask = await _userRepository.GetUsersCountAsync();
                //   await Task.WhenAll(usersTask, totalCountTask);
                var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersTask);
                return new PaginatedResponse<UserDto>
                {
                    Items = usersDto.ToList(),
                    TotalCount = totalCountTask,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                // Check if user already exists
                var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User with this email already exists");
                }
                // Check if username already exists
                var existingUserByUsername = await _userRepository.GetByUsernameAsync(createUserDto.Username);
                if (existingUserByUsername != null)
                {
                    throw new InvalidOperationException("User with this username already exists");
                }

                var user = _mapper.Map<User>(createUserDto);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("User created successfully: {UserId}", user.UserId);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user: {Email}", createUserDto.Email);
                throw;
            }
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }

                _mapper.Map(updateUserDto, user);
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("User updated successfully: {UserId}", userId);
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user: {UserId}", userId);
                throw;
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                await _userRepository.DeleteAsync(userId);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("User deleted successfully: {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetUserPermissionsAsync(int userId)
        {
            try
            {
                var permissions = await _userRepository.GetUserPermissionsAsync(userId);
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user permissions: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetUserEffectivePermissionsAsync(int userId)
        {
            try
            {
                var permissions = await _userRepository.GetUserEffectivePermissionsAsync(userId);
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user effective permissions: {UserId}", userId);
                throw;
            }
        }

        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            try
            {
                _logger.LogInformation("Assigning role {RoleId} to user {UserId}", roleId, userId);

                // Use the repository method to assign the role
                await _userRepository.AssignRoleToUserAsync(userId, roleId);

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
                _logger.LogInformation("Removing role {RoleId} from user {UserId}", roleId, userId);

                // Use the repository method to remove the role
                await _userRepository.RemoveRoleFromUserAsync(userId, roleId);

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
                _logger.LogInformation("Assigning permission {PermissionId} to user {UserId}", permissionId, userId);

                // Use the repository method to assign the permission
                await _userRepository.AssignPermissionToUserAsync(userId, permissionId, expiresAt);

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
                _logger.LogInformation("Removing permission {PermissionId} from user {UserId}", permissionId, userId);

                // Use the repository method to remove the permission
                await _userRepository.RemovePermissionFromUserAsync(userId, permissionId);

                _logger.LogInformation("Successfully removed permission {PermissionId} from user {UserId}", permissionId, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing permission from user: {UserId}, {PermissionId}", userId, permissionId);
                throw;
            }
        }

        // Additional service methods that leverage the repository
        public async Task<IEnumerable<Role>> GetUserRolesAsync(int userId)
        {
            try
            {
                _logger.LogInformation("Getting roles for user {UserId}", userId);
                return await _userRepository.GetUserRolesAsync(userId);
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
                _logger.LogInformation("Getting permissions for user {UserId}", userId);
                return await _userRepository.GetUserPermissionsAsync(userId, includeExpired);
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
                _logger.LogInformation("Getting all permissions (direct + role-based) for user {UserId}", userId);
                return await _userRepository.GetAllUserPermissionsAsync(userId, includeExpired);
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
                _logger.LogDebug("Checking if user {UserId} has permission {PermissionName}", userId, permissionName);
                return await _userRepository.UserHasPermissionAsync(userId, permissionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking permission for user: {UserId}, Permission: {PermissionName}", userId, permissionName);
                throw;
            }
        }

        public async Task<bool> HasPermissionAsync(int userId, string permissionName)
        {
            try
            {
                var permissions = await _userRepository.GetUserEffectivePermissionsAsync(userId);
                return permissions.Any(p => p.Name == permissionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user permission: {UserId}, {PermissionName}", userId, permissionName);
                throw;
            }
        }
    }
}


