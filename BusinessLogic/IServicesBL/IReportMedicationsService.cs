using BusinessLogic.DTOs.Drugs;
using BusinessLogic.DTOs.ReportMedication;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IReportMedicationsService
    {
        Task<ReportMedicationDto> CreateDrugAsync(CreateReportMedicationDto createReportMedicationDto);
        Task<ReportMedicationDto?> GetDrugByIdAsync(int id);
        Task<IEnumerable<ReportMedicationDto>> GetAllDrugsAsync();
        Task<IEnumerable<ReportMedicationSummaryDto>> GetDrugsSummaryAsync();
        Task<ReportMedicationDto> UpdateDrugAsync(UpdateReportMedicationDto updateReportMedicationDto);
        Task<bool> DeleteDrugAsync(int id);
        Task<PaginatedResponse<ReportMedicationDto>> GetPagedDrugsAsync(int page, int pageSize);
    }
}
