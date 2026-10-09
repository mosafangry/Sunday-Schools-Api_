using DataAccess.DatabaseContext;
using DataAccess.IRepository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class DrugReportRepository : IDrugReportRepository
    {
        private readonly PharmaDbContext _context;

        public DrugReportRepository(PharmaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedDrugReportDto> GetDrugReportAsync(DrugReportRequestDto request)
        {
            var query = _context.Drugs
        .SelectMany(d => d.Patients, (d, p) => new { Drug = d, Patient = p })
        .GroupJoin(_context.Reactions,
            dp => dp.Patient.Id,
            r => r.PatientId,
            (dp, r) => new { dp.Drug, dp.Patient, Reactions = r })
        .SelectMany(
            dpr => dpr.Reactions.DefaultIfEmpty(),
            (dpr, r) => new
            {
                Drug = dpr.Drug,
                Patient = dpr.Patient,
                Reaction = r,
                Icsr = dpr.Patient.Icsr // Explicitly include IcsrEntity
            })
        .Where(x => x.Icsr == null || !x.Icsr.IsDeleted) // Include non-deleted IcsrEntity or null Icsr
        .Select(x => new DrugReportDto
        {
            MedicinalProduct = x.Drug.MedicinalProduct,
            TradeName = x.Drug.TradeName,
            CaseId = x.Patient.CaseId,
            ReactionMeddraPT = x.Reaction != null ? x.Reaction.ReactionMeddraPT : null,
            NarrativeIncludeClinical = x.Patient.Summary != null ? x.Patient.Summary.NarrativeIncludeClinical : null
        });
            // Apply filters (using ToLower for case-insensitive filtering)
            if (!string.IsNullOrWhiteSpace(request.MedicinalProductFilter))
            {
                var filter = request.MedicinalProductFilter.ToLower();
                query = query.Where(dto => dto.MedicinalProduct != null && dto.MedicinalProduct.ToLower().Contains(filter));
            }
            if (!string.IsNullOrWhiteSpace(request.TradeNameFilter))
            {
                var filter = request.TradeNameFilter.ToLower();
                query = query.Where(dto => dto.TradeName != null && dto.TradeName.ToLower().Contains(filter));
            }
            if (!string.IsNullOrWhiteSpace(request.CaseIdFilter))
            {
                var filter = request.CaseIdFilter.ToLower();
                query = query.Where(dto => dto.CaseId != null && dto.CaseId.ToLower().Contains(filter));
            }

            if (!string.IsNullOrWhiteSpace(request.ReactionMeddraPTFilter))
            {
                var filter = request.ReactionMeddraPTFilter.ToLower();
                query = query.Where(dto => dto.ReactionMeddraPT != null && dto.ReactionMeddraPT.ToLower().Contains(filter));
            }

            if (!string.IsNullOrWhiteSpace(request.NarrativeIncludeClinicalFilter))
            {
                var filter = request.NarrativeIncludeClinicalFilter.ToLower();
                query = query.Where(dto => dto.NarrativeIncludeClinical != null && dto.NarrativeIncludeClinical.ToLower().Contains(filter));
            }


            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply pagination
            var pageNumber = Math.Max(1, request.PageNumber);
            var pageSize = Math.Max(1, request.PageSize);
            query = query
                .OrderBy(dto => dto.MedicinalProduct)
                .ThenBy(dto => dto.CaseId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Execute query
            var items = await query.ToListAsync();

            return new PagedDrugReportDto
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}
