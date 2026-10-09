using DataAccess.DatabaseContext;
using DataAccess.Entities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
 
    public class ReportMedicationRepository : IReportMedicationRepository
    {
        private readonly PharmaDbContext _context;
        private readonly ILogger<DrugRepository> _logger;


        public ReportMedicationRepository(PharmaDbContext context, ILogger<DrugRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<ReportMedication> InsertAsync(ReportMedication drug)
        {
            try
            {
                if (drug == null)
                    throw new ArgumentNullException(nameof(drug));

                drug.CreatedDate = DateTime.UtcNow;
                drug.ModifiedDate = DateTime.UtcNow;

                await _context.ReportMedications.AddAsync(drug);
                await _context.SaveChangesAsync();

                return drug;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ReportMedication?> GetByIdAsync(int id)
        {
            return await _context.ReportMedications
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<List<ReportMedication>> GetByIdsAsync(List<int> ids)
        {
            return await _context.ReportMedications
                .Where(d => ids.Contains(d.Id) && !d.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<ReportMedication>> GetAllAsync()
        {
            return await _context.ReportMedications
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<int>> GetExistingDrugIdsAsync(List<string> medicinalProductNames)
        {
            try
            {
                if (medicinalProductNames == null || !medicinalProductNames.Any())
                {
                    _logger.LogWarning("No medicinal product names provided for drug ID lookup.");
                    return new List<int>();
                }

                // Remove duplicates and null/empty strings to optimize query
                var uniqueNames = medicinalProductNames
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Select(name => name.Trim())
                    .Distinct()
                    .ToList();

                if (!uniqueNames.Any())
                {
                    _logger.LogWarning("No valid medicinal product names provided after filtering.");
                    return new List<int>();
                }

                _logger.LogInformation("Querying existing ReportMedications for {Count} medicinal product names.", uniqueNames.Count);

                // Query the ReportMedications table for IDs where MedicinalProduct matches
                var drugIds = await _context.ReportMedications
                    .Where(d => uniqueNames.Contains(d.MedicinalProduct) && !d.IsDeleted)
                    .Select(d => d.Id)
                    .ToListAsync();

                _logger.LogInformation("Found {DrugCount} existing ReportMedications for provided medicinal product names.", drugIds.Count);

                return drugIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error querying existing drug IDs for medicinal product names.");
                throw;
            }
        }
        public async Task<IEnumerable<ReportMedication>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.ReportMedications
                .Where(x => !x.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.ReportMedications.CountAsync(x => !x.IsDeleted);
        }

        public async Task<ReportMedication> UpdateAsync(ReportMedication drug)
        {
            if (drug == null)
                throw new ArgumentNullException(nameof(drug));

            var existingDrug = await _context.ReportMedications.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == drug.Id);
            if (existingDrug == null)
                throw new InvalidOperationException($"Drug with ID {drug.Id} not found");

            // Update properties
            existingDrug.TradeName = drug.TradeName;
            existingDrug.DrugCharacterization = drug.DrugCharacterization;
            existingDrug.MedicinalProduct = drug.MedicinalProduct;
            existingDrug.ActiveSubstanceName = drug.ActiveSubstanceName;
            existingDrug.ObtainDrugCountry = drug.ObtainDrugCountry;
            existingDrug.DrugAdministrationRoute = drug.DrugAdministrationRoute;
            existingDrug.DrugAuthorizationCountry = drug.DrugAuthorizationCountry;
            //  existingDrug.PatientId = drug.PatientId;
            existingDrug.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingDrug;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var drug = await _context.ReportMedications.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id); ;
            if (drug == null)
                return false;

            _context.ReportMedications.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.ReportMedications.AnyAsync(d => d.Id == id && !d.IsDeleted);
        }
    }

}
