using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.User;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<PaginatedResponse<UserDto>> GetAllUsersAsync(int pageNumber, int pageSize);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(int userId);
        Task<IEnumerable<PermissionDto>> GetUserPermissionsAsync(int userId);
        Task<IEnumerable<PermissionDto>> GetUserEffectivePermissionsAsync(int userId);
        Task AssignRoleToUserAsync(int userId, int roleId);
        Task RemoveRoleFromUserAsync(int userId, int roleId);
        Task AssignPermissionToUserAsync(int userId, int permissionId, DateTime? expiresAt = null);
        Task RemovePermissionFromUserAsync(int userId, int permissionId);
        Task<bool> HasPermissionAsync(int userId, string permissionName);
    }
}
