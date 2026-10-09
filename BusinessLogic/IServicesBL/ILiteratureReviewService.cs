using BusinessLogic.DTOs.LiteratureReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface ILiteratureReviewService
    {

        // LiteratureReview methods
        Task<PagedResultDto<LiteratureReviewDto>> GetLiteratureReviewsAsync(
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<long> journalIds,
            IEnumerable<long> publisherIds,
            IEnumerable<long> medicationIds,
            int page,
            int pageSize);
        Task<LiteratureReviewDto> CreateLiteratureReviewAsync(CreateLiteratureReviewDto dto);

        // Medication methods
        Task<IEnumerable<MedicationDto>> GetMedicationsAsync();
        Task<MedicationDto> GetMedicationAsync(long id);
        Task<MedicationDto> CreateMedicationAsync(CreateMedicationDto dto);
        Task<MedicationDto> UpdateMedicationAsync(long id, UpdateMedicationDto dto);
        Task BulkInsertMedicationsAsync(IEnumerable<CreateMedicationDto> dtos);
        Task DeleteMedicationAsync(long id);

        // Journal methods
        Task<IEnumerable<JournalDto>> GetJournalsAsync();
        Task<JournalDto> GetJournalAsync(long id);
        Task<JournalDto> CreateJournalAsync(CreateJournalDto dto);
        Task<JournalDto> UpdateJournalAsync(long id, UpdateJournalDto dto);
        Task DeleteJournalAsync(long id);
        Task<IEnumerable<JournalDto>> GetJournalsByPublisherIdsAsync(IEnumerable<long> publisherIds);

        // Publisher methods
        Task<IEnumerable<PublisherDto>> GetPublishersAsync();
        Task<PublisherDto> GetPublisherAsync(long id);
        Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto dto);
        Task<PublisherDto> UpdatePublisherAsync(long id, UpdatePublisherDto dto);
        Task DeletePublisherAsync(long id);
    }
}


