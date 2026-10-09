using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IDrugRepository
    {
        Task<DrugEntity> InsertAsync(DrugEntity drug);
        Task<DrugEntity?> GetByIdAsync(int id);
        Task<List<DrugEntity>> GetByIdsAsync(List<int> ids);
        Task<IEnumerable<DrugEntity>> GetAllAsync();
        Task<IEnumerable<DrugEntity>> GetByPatientIdAsync(int patientId);
        Task<DrugEntity> UpdateAsync(DrugEntity drug);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<DrugEntity>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<List<int>> GetExistingDrugIdsAsync(List<string> medicinalProductNames);
    }
}
