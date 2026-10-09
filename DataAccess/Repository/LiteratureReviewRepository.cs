using DataAccess.DatabaseContext;
using DataAccess.Entities.LiteratureReviewEntities;
using DataAccess.IRepository;


namespace DataAccess.Repository
{
    public class LiteratureReviewRepository : ILiteratureReviewRepository
    {
        private readonly PharmaDbContext _context;

        public LiteratureReviewRepository(PharmaDbContext context)
        {
            _context = context;
        }

        #region LiteratureReview Methods
        public async Task<(IEnumerable<LiteratureReview> Items, int TotalCount)> GetLiteratureReviewsAsync(
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<long> journalIds,
            IEnumerable<long> publisherIds,
            IEnumerable<long> medicationIds,
            int page,
            int pageSize)
        {
            var query = _context.LiteratureReviews
                .Include(lr => lr.Journal)
                .ThenInclude(j => j.Publisher)
                .Include(lr => lr.LiteratureReviewMedications)
                .ThenInclude(lrm => lrm.Medication).OrderByDescending(x=>x.PublishDate)
                .AsQueryable();

            if (startDate.HasValue)
            {
                query = query.Where(lr => lr.PublishDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(lr => lr.PublishDate <= endDate.Value);
            }

            if (journalIds != null && journalIds.Any())
            {
                query = query.Where(lr => journalIds.Contains(lr.JournalId));
            }

            if (publisherIds != null && publisherIds.Any())
            {
                query = query.Where(lr => publisherIds.Contains(lr.Journal.PublisherId));
            }

            if (medicationIds != null && medicationIds.Any())
            {
                query = query.Where(lr => lr.LiteratureReviewMedications
                    .Any(lrm => medicationIds.Contains(lrm.MedicationId)));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(lr => lr.PublishDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task AddLiteratureReviewAsync(LiteratureReview literatureReview)
        {
            await _context.LiteratureReviews.AddAsync(literatureReview);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Medication Methods
        public async Task<IEnumerable<Medication>> GetMedicationsAsync()
        {
            return await _context.Medications
                .OrderBy(m => m.TradeName)
                .ToListAsync();
        }

        public async Task<Medication> GetMedicationByIdAsync(long id)
        {
            return await _context.Medications
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddMedicationAsync(Medication medication)
        {
            await _context.Medications.AddAsync(medication);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMedicationAsync(Medication medication)
        {
            _context.Medications.Update(medication);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicationAsync(long id)
        {
            var medication = await _context.Medications.FindAsync(id);
            if (medication != null)
            {
                _context.Medications.Remove(medication);
                await _context.SaveChangesAsync();
            }
        }
        public async Task BulkInsertMedicationsAsync(IEnumerable<Medication> medications)
        {
            await _context.Medications.AddRangeAsync(medications);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Journal Methods
        public async Task<IEnumerable<Journal>> GetJournalsAsync()
        {
            return await _context.Journals
                .Include(j => j.Publisher)
                .OrderBy(j => j.Title)
                .ToListAsync();
        }

        public async Task<Journal> GetJournalByIdAsync(long id)
        {
            return await _context.Journals
                .Include(j => j.Publisher)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        public async Task AddJournalAsync(Journal journal)
        {
            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateJournalAsync(Journal journal)
        {
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJournalAsync(long id)
        {
            var journal = await _context.Journals.FindAsync(id);
            if (journal != null)
            {
                _context.Journals.Remove(journal);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Publisher Methods
        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await _context.Publishers
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Publisher> GetPublisherByIdAsync(long id)
        {
            return await _context.Publishers
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(long id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _context.Publishers.Remove(publisher);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
        public async Task<IEnumerable<Journal>> GetJournalsByPublisherIdsAsync(IEnumerable<long> publisherIds)
        {
            return await _context.Journals
                .Where(j => publisherIds.Contains(j.PublisherId))
                .Include(j => j.Publisher)
                .OrderBy(j => j.Title)
                .ToListAsync();
        }

    }
}