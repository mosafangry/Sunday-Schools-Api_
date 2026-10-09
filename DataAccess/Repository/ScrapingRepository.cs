using DataAccess.DatabaseContext;
using DataAccess.Entities.FdaValidation;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ScrapingRepository : IScrapingRepository
    {
        private readonly PharmaDbContext _context;
        public ScrapingRepository(PharmaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FDAValidationResult result)
        {
            _context.FDAValidationResults.Add(result);
            await _context.SaveChangesAsync();
        }

        public async Task<FDAValidationResult?> GetBySafetyReportIdAsync(string safetyReportId)
        {
            return await _context.FDAValidationResults
                .Include(r => r.Errors)
                .FirstOrDefaultAsync(r => r.SafetyReportId == safetyReportId);
        }

        public async Task<List<FDAValidationResult>> GetAllAsync()
        {
            return await _context.FDAValidationResults
                .Include(r => r.Errors)
                .ToListAsync();
        }

    }
}
