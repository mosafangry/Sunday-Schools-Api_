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
    public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(PharmaDbContext context, ILogger<PermissionRepository> logger)
            : base(context, logger)
        {
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByCategory(string category)
        {
            try
            {
                return await _dbSet
                    .Where(p => p.Category == category && p.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting permissions by category {Category}", category);
                throw;
            }
        }
    }
}
