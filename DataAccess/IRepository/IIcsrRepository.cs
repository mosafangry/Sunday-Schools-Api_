using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    //Individual Case Safety Reports
    public interface IIcsrRepository
    {
        Task<IcsrEntity> GetByIdAsync(int safetyReportId, int? companyId);
        Task<bool> CreateAsync(IcsrEntity icsrEntity);
        Task<bool> UpdateAsync(IcsrEntity icsrEntity, int userId, int? companyId);
        Task<bool> DeleteAsync(int safetyReportId, int userId, int? companyId);
        Task<IEnumerable<string>> GetAllIdsAsync();
        Task<IEnumerable<IcsrEntity>> GetAllAsync();
        Task<List<IcsrEntity>> GetPaginatedAsync(int pageNumber, int pageSize, int? companyId);
        Task<int> GetTotalCountAsync(int? companyId);
        Task<bool> CreateAsyncWithExistingDrugs(IcsrEntity icsrEntity, List<int> existingDrugIds);
        Task<bool> UpdateWithDrugsAsync(IcsrEntity icsrEntity, List<int> drugIds);
    }
}
