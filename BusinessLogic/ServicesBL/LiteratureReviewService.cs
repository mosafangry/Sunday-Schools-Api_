using AutoMapper;
using BusinessLogic.DTOs.LiteratureReviewDtos;
using BusinessLogic.IServicesBL;
using DataAccess.Entities.LiteratureReviewEntities;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class LiteratureReviewService : ILiteratureReviewService
    {
        private readonly ILiteratureReviewRepository _repository;
        private readonly IMapper _mapper;

        public LiteratureReviewService(ILiteratureReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region LiteratureReview Methods
        public async Task<PagedResultDto<LiteratureReviewDto>> GetLiteratureReviewsAsync(
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<long> journalIds,
            IEnumerable<long> publisherIds,
            IEnumerable<long> medicationIds,
            int page,
            int pageSize)
        {
            var (items, totalCount) = await _repository.GetLiteratureReviewsAsync(
                startDate, endDate, journalIds, publisherIds, medicationIds, page, pageSize);
            var dtos = _mapper.Map<IEnumerable<LiteratureReviewDto>>(items);
            return new PagedResultDto<LiteratureReviewDto>
            {
                Items = dtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<LiteratureReviewDto> CreateLiteratureReviewAsync(CreateLiteratureReviewDto dto)
        {
            var literatureReview = _mapper.Map<LiteratureReview>(dto);
            await _repository.AddLiteratureReviewAsync(literatureReview);
            return _mapper.Map<LiteratureReviewDto>(literatureReview);
        }
        #endregion

        #region Medication Methods
        public async Task<IEnumerable<MedicationDto>> GetMedicationsAsync()
        {
            var medications = await _repository.GetMedicationsAsync();
            return _mapper.Map<IEnumerable<MedicationDto>>(medications);
        }

        public async Task<MedicationDto> GetMedicationAsync(long id)
        {
            var medication = await _repository.GetMedicationByIdAsync(id);
            return medication != null ? _mapper.Map<MedicationDto>(medication) : null;
        }

        public async Task<MedicationDto> CreateMedicationAsync(CreateMedicationDto dto)
        {
            var medication = _mapper.Map<Medication>(dto);
            await _repository.AddMedicationAsync(medication);
            return _mapper.Map<MedicationDto>(medication);
        }

        public async Task<MedicationDto> UpdateMedicationAsync(long id, UpdateMedicationDto dto)
        {
            var medication = await _repository.GetMedicationByIdAsync(id);
            if (medication == null) return null;
            _mapper.Map(dto, medication);
            await _repository.UpdateMedicationAsync(medication);
            return _mapper.Map<MedicationDto>(medication);
        }

        public async Task DeleteMedicationAsync(long id)
        {
            await _repository.DeleteMedicationAsync(id);
        }
        public async Task BulkInsertMedicationsAsync(IEnumerable<CreateMedicationDto> dtos)
        {
            var medications = _mapper.Map<IEnumerable<Medication>>(dtos);
            await _repository.BulkInsertMedicationsAsync(medications);
        }
        #endregion

        #region Journal Methods
        public async Task<IEnumerable<JournalDto>> GetJournalsAsync()
        {
            var journals = await _repository.GetJournalsAsync();
            return _mapper.Map<IEnumerable<JournalDto>>(journals);
        }

        public async Task<JournalDto> GetJournalAsync(long id)
        {
            var journal = await _repository.GetJournalByIdAsync(id);
            return journal != null ? _mapper.Map<JournalDto>(journal) : null;
        }

        public async Task<JournalDto> CreateJournalAsync(CreateJournalDto dto)
        {
            var journal = _mapper.Map<Journal>(dto);
            await _repository.AddJournalAsync(journal);
            return _mapper.Map<JournalDto>(journal);
        }

        public async Task<JournalDto> UpdateJournalAsync(long id, UpdateJournalDto dto)
        {
            var journal = await _repository.GetJournalByIdAsync(id);
            if (journal == null) return null;
            _mapper.Map(dto, journal);
            await _repository.UpdateJournalAsync(journal);
            return _mapper.Map<JournalDto>(journal);
        }

        public async Task DeleteJournalAsync(long id)
        {
            await _repository.DeleteJournalAsync(id);
        }

        public async Task<IEnumerable<JournalDto>> GetJournalsByPublisherIdsAsync(IEnumerable<long> publisherIds)
        {
            var journals = await _repository.GetJournalsByPublisherIdsAsync(publisherIds);
            return _mapper.Map<IEnumerable<JournalDto>>(journals);
        }
        #endregion

        #region Publisher Methods
        public async Task<IEnumerable<PublisherDto>> GetPublishersAsync()
        {
            var publishers = await _repository.GetPublishersAsync();
            return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
        }

        public async Task<PublisherDto> GetPublisherAsync(long id)
        {
            var publisher = await _repository.GetPublisherByIdAsync(id);
            return publisher != null ? _mapper.Map<PublisherDto>(publisher) : null;
        }

        public async Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto dto)
        {
            var publisher = _mapper.Map<Publisher>(dto);
            await _repository.AddPublisherAsync(publisher);
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task<PublisherDto> UpdatePublisherAsync(long id, UpdatePublisherDto dto)
        {
            var publisher = await _repository.GetPublisherByIdAsync(id);
            if (publisher == null) return null;
            _mapper.Map(dto, publisher);
            await _repository.UpdatePublisherAsync(publisher);
            return _mapper.Map<PublisherDto>(publisher);
        }

        public async Task DeletePublisherAsync(long id)
        {
            await _repository.DeletePublisherAsync(id);
        }
        #endregion
    }   
}
