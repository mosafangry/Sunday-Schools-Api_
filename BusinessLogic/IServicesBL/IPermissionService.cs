using BusinessLogic.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IPermissionService
    {
        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);
        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
        Task<IEnumerable<PermissionDto>> GetPermissionsByCategoryAsync(string category);
        Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto createPermissionDto);
        Task<PermissionDto> UpdatePermissionAsync(int permissionId, UpdatePermissionDto updatePermissionDto);
        Task DeletePermissionAsync(int permissionId);
    }

}
