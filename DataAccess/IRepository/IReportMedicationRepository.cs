using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IReportMedicationRepository
    {
        Task<ReportMedication> InsertAsync(ReportMedication drug);
        Task<ReportMedication?> GetByIdAsync(int id);
        Task<List<ReportMedication>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<ReportMedication>> GetAllAsync();
        Task<ReportMedication> UpdateAsync(ReportMedication drug);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<ReportMedication>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<List<int>> GetExistingDrugIdsAsync(List<string> medicinalProductNames);
    }

}
