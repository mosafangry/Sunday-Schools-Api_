using DataAccess.DatabaseContext;
using DataAccess.Entities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repository
{
    public class DrugRepository : IDrugRepository
    {
        private readonly PharmaDbContext _context;
        private readonly ILogger<DrugRepository> _logger;


        public DrugRepository(PharmaDbContext context, ILogger<DrugRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
        }

        public async Task<DrugEntity> InsertAsync(DrugEntity drug)
        {
            try
            {
                if (drug == null)
                    throw new ArgumentNullException(nameof(drug));

                drug.CreatedDate = DateTime.UtcNow;
                drug.ModifiedDate = DateTime.UtcNow;

                await _context.Drugs.AddAsync(drug);
                await _context.SaveChangesAsync();

                return drug;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DrugEntity?> GetByIdAsync(int id)
        {
            return await _context.Drugs
                .Include(d => d.Patients)
                .Include(d => d.ActiveSubstance)
                .Include(d => d.DrugReactionRelatedness)
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);
        }

        public async Task<List<DrugEntity>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Drugs
                .Where(d => ids.Contains(d.Id) && !d.IsDeleted)
                .Include(d => d.Patients)
                .Include(d => d.ActiveSubstance)
                .Include(d => d.DrugReactionRelatedness)
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugEntity>> GetAllAsync()
        {
            return await _context.Drugs
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugEntity>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Patients
                .Where(p => p.Id == patientId)
                .SelectMany(p => p.Drugs.Where(d => !d.IsDeleted))
                .Include(d => d.ActiveSubstance)
                .Include(d => d.DrugReactionRelatedness)
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

                _logger.LogInformation("Querying existing drugs for {Count} medicinal product names.", uniqueNames.Count);

                // Query the Drugs table for IDs where MedicinalProduct matches
                var drugIds = await _context.Drugs
                    .Where(d => uniqueNames.Contains(d.MedicinalProduct) && !d.IsDeleted)
                    .Select(d => d.Id)
                    .ToListAsync();

                _logger.LogInformation("Found {DrugCount} existing drugs for provided medicinal product names.", drugIds.Count);

                return drugIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error querying existing drug IDs for medicinal product names.");
                throw;
            }
        }


        public async Task<IEnumerable<DrugEntity>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Drugs
                .Include(d => d.Patients)
                .Include(d => d.ActiveSubstance)
                .Where(x => !x.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Drugs.CountAsync(x => !x.IsDeleted);
        }

        public async Task<DrugEntity> UpdateAsync(DrugEntity drug)
        {
            if (drug == null)
                throw new ArgumentNullException(nameof(drug));

            var existingDrug = await _context.Drugs.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == drug.Id);
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
            existingDrug.DrugStartDateFormat = drug.DrugStartDateFormat;
            existingDrug.DrugStartDate = drug.DrugStartDate;
            existingDrug.DrugEndDateFormat = drug.DrugEndDateFormat;
            existingDrug.DrugEndDate = drug.DrugEndDate;
            existingDrug.DrugIndicationMeddraVersion = drug.DrugIndicationMeddraVersion;
            existingDrug.DrugIndication = drug.DrugIndication;
            existingDrug.ActionDrug = drug.ActionDrug;
            existingDrug.DrugRecurReadministration = drug.DrugRecurReadministration;
            //  existingDrug.PatientId = drug.PatientId;
            existingDrug.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingDrug;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var drug = await _context.Drugs.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id); ;
            if (drug == null)
                return false;

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Drugs.AnyAsync(d => d.Id == id && !d.IsDeleted);
        }
    }
}
