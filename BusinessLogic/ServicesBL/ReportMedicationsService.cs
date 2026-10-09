using AutoMapper;
using BusinessLogic.DTOs.Drugs;
using BusinessLogic.DTOs.ReportMedication;
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

    public class ReportMedicationsService : IReportMedicationsService
    {
        private readonly IReportMedicationRepository _drugRepository;
        private readonly IMapper _mapper;

        public ReportMedicationsService(IReportMedicationRepository drugRepository, IMapper mapper)
        {
            _drugRepository = drugRepository ?? throw new ArgumentNullException(nameof(drugRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ReportMedicationDto> CreateDrugAsync(CreateReportMedicationDto createReportMedicationDto)
        {

            // Map DTO to Entity
            try
            {
                if (createReportMedicationDto == null)
                    throw new ArgumentNullException(nameof(createReportMedicationDto));

                // Additional business validation
                ValidateCreateDrug(createReportMedicationDto);
                var ReportMedication = _mapper.Map<ReportMedication>(createReportMedicationDto);

                // Create the drug
                var createdDrug = await _drugRepository.InsertAsync(ReportMedication);

                // Map back to DTO and return
                return _mapper.Map<ReportMedicationDto>(createdDrug);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ReportMedicationDto?> GetDrugByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than 0", nameof(id));

            var drug = await _drugRepository.GetByIdAsync(id);
            return drug != null ? _mapper.Map<ReportMedicationDto>(drug) : null;
        }

        public async Task<IEnumerable<ReportMedicationDto>> GetAllDrugsAsync()
        {
            var drugs = await _drugRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReportMedicationDto>>(drugs);
        }

        public async Task<IEnumerable<ReportMedicationSummaryDto>> GetDrugsSummaryAsync()
        {
            try
            {

                var drugs = await _drugRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ReportMedicationSummaryDto>>(drugs);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ReportMedicationDto> UpdateDrugAsync(UpdateReportMedicationDto updateReportMedicationDto)
        {
            if (updateReportMedicationDto == null)
                throw new ArgumentNullException(nameof(updateReportMedicationDto));

            // Check if drug exists
            var existingDrug = await _drugRepository.GetByIdAsync(updateReportMedicationDto.Id);
            if (existingDrug == null)
                throw new InvalidOperationException($"Drug with ID {updateReportMedicationDto.Id} not found");

            // Additional business validation
            ValidateUpdateDrug(updateReportMedicationDto);

            // Map DTO to Entity
            var ReportMedication = _mapper.Map<ReportMedication>(updateReportMedicationDto);

            // Update the drug
            var updatedDrug = await _drugRepository.UpdateAsync(ReportMedication);

            // Map back to DTO and return
            return _mapper.Map<ReportMedicationDto>(updatedDrug);
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

        public async Task<PaginatedResponse<ReportMedicationDto>> GetPagedDrugsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page must be greater than 0", nameof(pageNumber));

            if (pageSize <= 0 || pageSize > 100)
                throw new ArgumentException("Page size must be between 1 and 100", nameof(pageSize));

            var drugs = await _drugRepository.GetPagedAsync(pageNumber, pageSize);
            var totalCount = await _drugRepository.GetTotalCountAsync();

            var ReportMedicationDtos = _mapper.Map<IEnumerable<ReportMedicationDto>>(drugs);
            return new PaginatedResponse<ReportMedicationDto>
            {
                Items = ReportMedicationDtos.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        #region Private Validation Methods

        private void ValidateCreateDrug(CreateReportMedicationDto createReportMedicationDto)
        {
            if (string.IsNullOrWhiteSpace(createReportMedicationDto.MedicinalProduct))
                throw new ArgumentException("Medicinal Product is required");
            ValidateDrugCharacterization(createReportMedicationDto.DrugCharacterization);
        }

        private void ValidateUpdateDrug(UpdateReportMedicationDto updateReportMedicationDto)
        {
            if (string.IsNullOrWhiteSpace(updateReportMedicationDto.MedicinalProduct))
                throw new ArgumentException("Medicinal Product is required");
            ValidateDrugCharacterization(updateReportMedicationDto.DrugCharacterization);
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
