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
   
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(PharmaDbContext context, ILogger<RoleRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<Role> GetRoleWithPermissionsAsync(int roleId)
        {
            try
            {
                return await _dbSet
                    .Include(r => r.RolePermissions)
                    .ThenInclude(rp => rp.Permission)
                    .FirstOrDefaultAsync(r => r.RoleId == roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting role with permissions for roleId {RoleId}", roleId);
                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetRolePermissionsAsync(int roleId)
        {
            try
            {
                return await _context.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Include(rp => rp.Permission)
                    .Select(rp => rp.Permission)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting role permissions for roleId {RoleId}", roleId);
                throw;
            }
        }

        public async Task AssignPermissionToRoleAsync(int roleId, int permissionId)
        {
            try
            {
                var exists = await _context.RolePermissions
                    .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (!exists)
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId
                    };
                    await _context.RolePermissions.AddAsync(rolePermission);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning permission {PermissionId} to role {RoleId}", permissionId, roleId);
                throw;
            }
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            try
            {
                var rolePermission = await _context.RolePermissions
                    .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

                if (rolePermission != null)
                {
                    _context.RolePermissions.Remove(rolePermission);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing permission {PermissionId} from role {RoleId}", permissionId, roleId);
                throw;
            }
        }
    }
}
