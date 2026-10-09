using BusinessLogic.DTOs.MedDRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IMedDRAService
    {
        Task<MedDRASearchResponse> SearchTermsAsync(MedDRASearchRequest request);
        Task<MedDRAHierarchyDto> GetTermByCodeAsync(MedDRATermRequest request);
        Task<MedDRASearchResponse> SearchOrGetTermAsync(MedDRASearchUnifiedRequest request);
        Task<MedDRASearchResponse> GetTermsBySOCAsync(MedDRASOCRequest request);
        Task<List<string>> GetAllSOCsAsync();
        Task<bool> ValidateTermCodeAsync(string termCode, string termLevel);
        Task<List<MedicationSignalDto>> DetectMedicationSignalAsync(string medicationName);
    }
}
