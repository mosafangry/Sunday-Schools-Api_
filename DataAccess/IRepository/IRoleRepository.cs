using DataAccess.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetRoleWithPermissionsAsync(int roleId);
        Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId);
        Task AssignPermissionToRoleAsync(int roleId, int permissionId);
        Task RemovePermissionFromRoleAsync(int roleId, int permissionId);
    }
}
