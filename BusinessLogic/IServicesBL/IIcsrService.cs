using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IIcsrService
    {
        Task<IchIcsrXMLModel> GetIcsrByIdAsync(int safetyReportId, int? companyId);
        Task<IchIcsrXMLModel> CreateIcsrAsync(IchIcsrXMLModel importedIcsr, int userId, int? CompanyId);
        Task<IEnumerable<string>> GetAllIcsrIdsAsync();
        Task<bool> UpdateIcsrAsync(IchIcsrXMLModel updatedIcsr, int userId, int? companyId);
        Task<bool> DeleteIcsrAsync(int safetyReportId, int userId, int? companyId);
        Task<PaginatedResponse<IchIcsrXMLModel>> GetPaginatedIcsrsAsync(int pageNumber, int pageSize, int? companyId);
        Dictionary<string, Dictionary<string, int>> GetAllEnums();
        Task<bool> CreateIcsrWithExistingDrugsAsync(IchIcsrXMLModelWithDrugIds importedIcsr, List<int> existingDrugIds, int userId, int? companyId);
        Task<bool> UpdateIcsrWithDrugsAsync(int icsrId, IchIcsrXMLModelWithDrugIds updatedIcsr, List<int> drugIds, int userId, int? companyId);
        Task<IchIcsrXMLModel> MapToIchIcsrXMLModel(IchIcsrXMLModelWithDrugIds source);
    }
}
