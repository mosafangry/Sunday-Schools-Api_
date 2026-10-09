using BusinessLogic.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IRoleService
    {
        Task<RoleDto> GetRoleByIdAsync(int roleId);
        Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto);
        Task<RoleDto> UpdateRoleAsync(int roleId, UpdateRoleDto updateRoleDto);
        Task DeleteRoleAsync(int roleId);
        Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(int roleId);
        Task AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }

}
