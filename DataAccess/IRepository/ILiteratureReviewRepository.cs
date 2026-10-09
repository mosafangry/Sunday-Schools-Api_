using DataAccess.Entities.LiteratureReviewEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ILiteratureReviewRepository
    {

        // LiteratureReview methods
        Task<(IEnumerable<LiteratureReview> Items, int TotalCount)> GetLiteratureReviewsAsync(
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<long> journalIds,
            IEnumerable<long> publisherIds,
            IEnumerable<long> medicationIds,
            int page,
            int pageSize);
        Task AddLiteratureReviewAsync(LiteratureReview literatureReview);

        // Medication methods
        Task<IEnumerable<Medication>> GetMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(long id);
        Task AddMedicationAsync(Medication medication);
        Task UpdateMedicationAsync(Medication medication);
        Task DeleteMedicationAsync(long id);
        Task BulkInsertMedicationsAsync(IEnumerable<Medication> medications); // Added for seeding

        // Journal methods
        Task<IEnumerable<Journal>> GetJournalsAsync();
        Task<Journal> GetJournalByIdAsync(long id);
        Task AddJournalAsync(Journal journal);
        Task UpdateJournalAsync(Journal journal);
        Task DeleteJournalAsync(long id);
        Task<IEnumerable<Journal>> GetJournalsByPublisherIdsAsync(IEnumerable<long> publisherIds);
        // Publisher methods
        Task<IEnumerable<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(long id);
        Task AddPublisherAsync(Publisher publisher);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(long id);

    }
}