using AutoMapper;
using BusinessLogic.DTOs.Drugs;
using BusinessLogic.IServicesBL;
using DataAccess.Entities;
using DataAccess.IRepository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;
        private readonly IMapper _mapper;

        public DrugService(IDrugRepository drugRepository, IMapper mapper)
        {
            _drugRepository = drugRepository ?? throw new ArgumentNullException(nameof(drugRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<DrugDto> CreateDrugAsync(CreateDrugDto createDrugDto)
        {
            if (createDrugDto == null)
                throw new ArgumentNullException(nameof(createDrugDto));

            // Additional business validation
            ValidateCreateDrug(createDrugDto);

            // Map DTO to Entity
            var drugEntity = _mapper.Map<DrugEntity>(createDrugDto);

            // Create the drug
            var createdDrug = await _drugRepository.InsertAsync(drugEntity);

            // Map back to DTO and return
            return _mapper.Map<DrugDto>(createdDrug);
        }

        public async Task<DrugDto?> GetDrugByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            var drug = await _drugRepository.GetByIdAsync(id);
            return drug != null ? _mapper.Map<DrugDto>(drug) : null;
        }

        public async Task<IEnumerable<DrugDto>> GetAllDrugsAsync()
        {
            var drugs = await _drugRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DrugDto>>(drugs);
        }

        public async Task<IEnumerable<DrugSummaryDto>> GetDrugsSummaryAsync()
        {
            var drugs = await _drugRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DrugSummaryDto>>(drugs);
        }

        public async Task<IEnumerable<DrugDto>> GetDrugsByPatientIdAsync(int patientId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Patient ID must be greater than 0", nameof(patientId));

            var drugs = await _drugRepository.GetByPatientIdAsync(patientId);
            return _mapper.Map<IEnumerable<DrugDto>>(drugs);
        }

        public async Task<DrugDto> UpdateDrugAsync(UpdateDrugDto updateDrugDto)
        {
            if (updateDrugDto == null)
                throw new ArgumentNullException(nameof(updateDrugDto));

            // Check if drug exists
            var existingDrug = await _drugRepository.GetByIdAsync(updateDrugDto.Id);
            if (existingDrug == null)
                throw new InvalidOperationException($"Drug with ID {updateDrugDto.Id} not found");

            // Additional business validation
            ValidateUpdateDrug(updateDrugDto);

            // Map DTO to Entity
            var drugEntity = _mapper.Map<DrugEntity>(updateDrugDto);

            // Update the drug
            var updatedDrug = await _drugRepository.UpdateAsync(drugEntity);

            // Map back to DTO and return
            return _mapper.Map<DrugDto>(updatedDrug);
        }

        public async Task<bool> DeleteDrugAsync(int id)
        {
            var existingDrug = await _drugRepository.GetByIdAsync(id);
            if (existingDrug == null)
                throw new InvalidOperationException($"Drug with ID {id} not found");
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            return await _drugRepository.DeleteAsync(id);
        }

        public async Task<PaginatedResponse<DrugDto>> GetPagedDrugsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page must be greater than 0", nameof(pageNumber));

            if (pageSize <= 0 || pageSize > 100)
                throw new ArgumentException("Page size must be between 1 and 100", nameof(pageSize));

            var drugs = await _drugRepository.GetPagedAsync(pageNumber, pageSize);
            var totalCount = await _drugRepository.GetTotalCountAsync();

            var drugDtos = _mapper.Map<IEnumerable<DrugDto>>(drugs);
            return new PaginatedResponse<DrugDto>
            {
                Items = drugDtos.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        #region Private Validation Methods

        private void ValidateCreateDrug(CreateDrugDto createDrugDto)
        {
            if (string.IsNullOrWhiteSpace(createDrugDto.MedicinalProduct))
                throw new ArgumentException("Medicinal Product is required");
            ValidateDateFormats(createDrugDto.DrugStartDateFormat, createDrugDto.DrugEndDateFormat);
            ValidateDrugCharacterization(createDrugDto.DrugCharacterization);
        }

        private void ValidateUpdateDrug(UpdateDrugDto updateDrugDto)
        {
            if (string.IsNullOrWhiteSpace(updateDrugDto.MedicinalProduct))
                throw new ArgumentException("Medicinal Product is required");
            ValidateDateFormats(updateDrugDto.DrugStartDateFormat, updateDrugDto.DrugEndDateFormat);
            ValidateDrugCharacterization(updateDrugDto.DrugCharacterization);
        }

        private void ValidateDateFormats(int? startDateFormat, int? endDateFormat)
        {
            if (startDateFormat.HasValue && (startDateFormat.Value < 1 || startDateFormat.Value > 610))
                throw new ArgumentException("Invalid start date format");

            if (endDateFormat.HasValue && (endDateFormat.Value < 1 || endDateFormat.Value > 610))
                throw new ArgumentException("Invalid end date format");
        }

        private void ValidateDrugCharacterization(int? drugCharacterization)
        {
            if (drugCharacterization.HasValue && (drugCharacterization.Value < 1 || drugCharacterization.Value > 3))
                throw new ArgumentException("Drug characterization must be between 1 and 3");
        }

        #endregion
    }
}
