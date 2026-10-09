using BusinessLogic.DTOs.Drugs;
using DataAccess.Entities;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IDrugService
    {
        Task<DrugDto> CreateDrugAsync(CreateDrugDto createDrugDto);
        Task<DrugDto?> GetDrugByIdAsync(int id);
        Task<IEnumerable<DrugDto>> GetAllDrugsAsync();
        Task<IEnumerable<DrugSummaryDto>> GetDrugsSummaryAsync();
        Task<IEnumerable<DrugDto>> GetDrugsByPatientIdAsync(int patientId);
        Task<DrugDto> UpdateDrugAsync(UpdateDrugDto updateDrugDto);
        Task<bool> DeleteDrugAsync(int id);
        Task<PaginatedResponse<DrugDto>> GetPagedDrugsAsync(int page, int pageSize);
    }
}
