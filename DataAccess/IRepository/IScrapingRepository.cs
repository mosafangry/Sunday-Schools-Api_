using DataAccess.Entities.FdaValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IScrapingRepository
    {
        Task AddAsync(FDAValidationResult result);
        Task<FDAValidationResult?> GetBySafetyReportIdAsync(string safetyReportId);
        Task<List<FDAValidationResult>> GetAllAsync();
    }
}
