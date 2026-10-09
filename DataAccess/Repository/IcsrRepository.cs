using DataAccess.DatabaseContext;
using DataAccess.Entities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;

namespace DataAccess.Repositories
{

    public class IcsrRepository : IIcsrRepository
    {
        private readonly PharmaDbContext _dbContext;
        private readonly ILogger<IcsrRepository> _logger;

        public IcsrRepository(PharmaDbContext dbContext, ILogger<IcsrRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IcsrEntity> GetByIdAsync(int safetyReportId, int? companyId)
        {
            try
            {
                _logger.LogDebug("Retrieving ICSR entity with safety report ID: {SafetyReportId}", safetyReportId);

                return await _dbContext.IcsrReports
                    .Include(i => i.MessageHeader)
                    .Include(i => i.PrimarySource)
                    .Include(i => i.Sender)
                    .Include(i => i.Receiver)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Reactions)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.ActiveSubstance)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.DrugReactionRelatedness)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Summary)
                    .Include(i => i.ConcomitantMedications)
                    .Include(i => i.ConcomitantDiseases).Where(x => x.CompanyId == companyId && !x.IsDeleted)
                    .FirstOrDefaultAsync(i => i.Id == safetyReportId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ICSR entity with safety report ID: {SafetyReportId}", safetyReportId);
                throw;
            }
        }

        public async Task<bool> CreateAsync(IcsrEntity icsrEntity)
        {
            try
            {
                if (icsrEntity == null)
                {
                    _logger.LogWarning("Attempted to create a null ICSR entity");
                    return false;
                }

                _logger.LogDebug("Creating ICSR entity with safety report ID: {SafetyReportId}", icsrEntity.SafetyReportId);

                await _dbContext.IcsrReports.AddAsync(icsrEntity);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ICSR entity with safety report ID: {SafetyReportId}",
                    icsrEntity?.SafetyReportId ?? "unknown");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(IcsrEntity icsrEntity, int userId, int? companyId)
        {
            try
            {
                if (icsrEntity == null)
                {
                    _logger.LogWarning("Attempted to update a null ICSR entity");
                    return false;
                }

                _logger.LogDebug("Updating ICSR entity with safety report ID: {SafetyReportId}", icsrEntity.SafetyReportId);

                // Check if entity exists (without tracking)
                var existsCheck = await _dbContext.IcsrReports
                    .AsNoTracking()
                    .AnyAsync(i => i.Id == icsrEntity.Id && i.CompanyId == companyId);

                if (!existsCheck)
                {
                    _logger.LogWarning("ICSR entity with safety report ID: {SafetyReportId} not found for update",
                        icsrEntity.SafetyReportId);
                    return false;
                }
                var trackedEntity = _dbContext.ChangeTracker.Entries<IcsrEntity>().FirstOrDefault(e => e.Entity.Id == icsrEntity.Id);
                if (trackedEntity != null)
                {
                    trackedEntity.State = EntityState.Detached;
                }

                // Set audit fields
                icsrEntity.CompanyId = companyId;
                icsrEntity.ModifiedByUserId = userId;
                icsrEntity.ModifiedDate = DateTime.UtcNow; // Assuming this exists in AuditableEntity

                // Update the entity (EF will track this as modified)
                _dbContext.IcsrReports.Update(icsrEntity);

                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ICSR entity with safety report ID: {SafetyReportId}",
                    icsrEntity?.SafetyReportId ?? "unknown");
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int safetyReportId, int userId, int? companyId)
        {
            try
            {
                _logger.LogDebug("Deleting ICSR entity with safety report ID: {SafetyReportId}", safetyReportId);

                var entityToDelete = await _dbContext.IcsrReports
                    .FirstOrDefaultAsync(i => i.Id == safetyReportId && i.CompanyId == companyId);

                if (entityToDelete == null)
                {
                    _logger.LogWarning("ICSR entity with safety report ID: {SafetyReportId} not found for deletion",
                        safetyReportId);
                    return false;
                }

                _dbContext.IcsrReports.Remove(entityToDelete);
                var result = await _dbContext.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleteting ICSR entity with safety report ID: {SafetyReportId}",
                    safetyReportId);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAllIdsAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all safety report IDs from IcsrReports");

                return await _dbContext.IcsrReports
                    .Select(i => i.SafetyReportId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all safety report IDs from IcsrReports");
                throw;
            }
        }
        public async Task<IEnumerable<IcsrEntity>> GetAllAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all ICSR entities");

                return await _dbContext.IcsrReports
                    .Include(i => i.MessageHeader)
                    .Include(i => i.PrimarySource)
                    .Include(i => i.Sender)
                    .Include(i => i.Receiver)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Reactions)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.ActiveSubstance)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.DrugReactionRelatedness)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Summary)
                    .Include(i => i.ConcomitantMedications)
                    .Include(i => i.ConcomitantDiseases)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all ICSR entities");
                throw;
            }
        }
        public async Task<List<IcsrEntity>> GetPaginatedAsync(int pageNumber, int pageSize, int? companyId)
        {
            try
            {
                _logger.LogDebug("Retrieving paginated ICSR entities: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);

                return await _dbContext.IcsrReports
                    .Include(i => i.MessageHeader)
                    .Include(i => i.PrimarySource)
                    .Include(i => i.Sender)
                    .Include(i => i.Receiver)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Reactions)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.ActiveSubstance)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                            .ThenInclude(d => d.DrugReactionRelatedness)
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Summary)
                    .Include(i => i.ConcomitantMedications)
                    .Include(i => i.ConcomitantDiseases).Where(x => x.CompanyId == companyId && !x.IsDeleted)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated ICSR entities");
                throw;
            }
        }
        public async Task<int> GetTotalCountAsync(int? companyId)
        {
            try
            {
                _logger.LogDebug("Retrieving total count of ICSR entities");

                return await _dbContext.IcsrReports.Where(x => x.CompanyId == companyId && !x.IsDeleted).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving total count of ICSR entities");
                throw;
            }
        }

        //public async Task<bool> CreateAsyncWithExistingDrugs_(IcsrEntity icsrEntity, List<int> existingDrugIds)
        //{
        //    using var transaction = await _dbContext.Database.BeginTransactionAsync();

        //    try
        //    {
        //        if (icsrEntity == null)
        //        {
        //            _logger.LogWarning("Attempted to create a null ICSR entity");
        //            return false;
        //        }

        //        _logger.LogDebug("Creating ICSR entity with existing drugs. SafetyReportId: {SafetyReportId}, DrugCount: {DrugCount}",
        //            icsrEntity.SafetyReportId, existingDrugIds?.Count ?? 0);

        //        // Step 1: Clear any drugs from the entity (we'll link existing ones)
        //        if (icsrEntity.Patient?.Drugs != null)
        //        {
        //            icsrEntity.Patient.Drugs.Clear();
        //        }

        //        // Step 2: Add ICSR entity
        //        await _dbContext.IcsrReports.AddAsync(icsrEntity);
        //        var result = await _dbContext.SaveChangesAsync();

        //        if (result <= 0)
        //        {
        //            await transaction.RollbackAsync();
        //            return false;
        //        }

        //        // Step 3: Link existing drugs to the patient if provided
        //        if (existingDrugIds?.Any() == true && icsrEntity.Patient != null)
        //        {
        //            // Validate drugs exist and belong to same company
        //            var validDrugs = await _dbContext.Drugs
        //                .Where(d => existingDrugIds.Contains(d.Id) &&
        //                           d.CompanyId == icsrEntity.CompanyId)
        //                .CountAsync();

        //            if (validDrugs != existingDrugIds.Count)
        //            {
        //                _logger.LogWarning("Some drugs not found or already linked. Expected: {Expected}, Found: {Found}",
        //                    existingDrugIds.Count, validDrugs);
        //                await transaction.RollbackAsync();
        //                return false;
        //            }

        //            // Update existing drugs with new patient ID
        //            await _dbContext.Drugs
        //                .Where(d => existingDrugIds.Contains(d.Id))
        //                .ExecuteUpdateAsync(d => d
        //                    .SetProperty(x => x.PatientId, icsrEntity.Patient.Id)
        //                    .SetProperty(x => x.ModifiedByUserId, icsrEntity.CreatedByUserId)
        //                    .SetProperty(x => x.ModifiedDate, DateTime.UtcNow));

        //            _logger.LogDebug("Successfully linked {Count} existing drugs to ICSR patient", existingDrugIds.Count);
        //        }

        //        await transaction.CommitAsync();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync();
        //        _logger.LogError(ex, "Error creating ICSR entity with existing drugs. SafetyReportId: {SafetyReportId}",
        //            icsrEntity?.SafetyReportId ?? "unknown");
        //        throw;
        //    }
        //}
        public async Task<bool> CreateAsyncWithExistingDrugs(IcsrEntity icsrEntity, List<int> existingDrugIds)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (icsrEntity == null)
                {
                    _logger.LogWarning("Attempted to create a null ICSR entity");
                    return false;
                }

                _logger.LogDebug("Creating ICSR entity with existing drugs. SafetyReportId: {SafetyReportId}, DrugCount: {DrugCount}",
                    icsrEntity.SafetyReportId, existingDrugIds?.Count ?? 0);

                // Step 1: Clear any drugs from the entity (we'll link existing ones)
                icsrEntity.Patient ??= new PatientEntity(); // Ensure it's not null
                icsrEntity.Patient.Drugs ??= new List<DrugEntity>();

                icsrEntity.Patient.Drugs.Clear();

                // Step 2: Add ICSR entity
                await _dbContext.IcsrReports.AddAsync(icsrEntity);
                var result = await _dbContext.SaveChangesAsync();

                if (result <= 0)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                // Step 3: Link existing drugs to the patient if provided
                if (existingDrugIds?.Any() == true && icsrEntity.Patient != null)
                {
                    var validDrugs = await _dbContext.Drugs
                        .Where(d => existingDrugIds.Contains(d.Id) &&
                                    (d.CompanyId == icsrEntity.CompanyId || d.CompanyId == null) &&
                                    !d.IsDeleted)
                        .ToListAsync();

                    if (validDrugs.Count != existingDrugIds.Count)
                    {
                        _logger.LogWarning("Some drugs not found or already linked. Expected: {Expected}, Found: {Found}",
                            existingDrugIds.Count, validDrugs.Count);
                        await transaction.RollbackAsync();
                        return false;
                    }

                    foreach (var drug in validDrugs)
                    {
                        icsrEntity.Patient.Drugs.Add(drug);
                    }

                    await _dbContext.SaveChangesAsync();

                    _logger.LogDebug("Successfully linked {Count} existing drugs to ICSR patient", validDrugs.Count);
                }

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error creating ICSR entity with existing drugs. SafetyReportId: {SafetyReportId}",
                    icsrEntity?.SafetyReportId ?? "unknown");
                throw;
            }
        }

        public async Task<bool> UpdateWithDrugsAsync(IcsrEntity icsrEntity, List<int> drugIds)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Get existing ICSR with all related data
                var existingIcsr = await _dbContext.IcsrReports
                    .Include(i => i.Patient)
                        .ThenInclude(p => p.Drugs)
                    .Include(i => i.MessageHeader)
                    .Include(i => i.PrimarySource)
                    .Include(i => i.Sender)
                    .Include(i => i.Receiver)
                    .Include(i => i.ConcomitantMedications)
                    .Include(i => i.ConcomitantDiseases)
                    .FirstOrDefaultAsync(i => i.Id == icsrEntity.Id);

                if (existingIcsr == null)
                {
                    return false;
                }

                // Update main ICSR properties
                UpdateIcsrProperties(existingIcsr, icsrEntity);

                // Update related entities
                await UpdateRelatedEntitiesAsync(existingIcsr, icsrEntity);

                // Handle drug associations
                await UpdateDrugAssociationsAsync(existingIcsr, drugIds);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating ICSR with drugs in repository");
                throw;
            }
        }
        private void UpdateIcsrProperties(IcsrEntity existing, IcsrEntity updated)
        {
            // Update main ICSR properties
            existing.Language = updated.Language;
            existing.SafetyReportId = updated.SafetyReportId;
            existing.SafetyReportVersion = updated.SafetyReportVersion;
            existing.PrimarySourceCountry = updated.PrimarySourceCountry;
            existing.OccurCountry = updated.OccurCountry;
            existing.TransmissionDateFormat = updated.TransmissionDateFormat;
            existing.TransmissionDate = updated.TransmissionDate;
            existing.ReportType = updated.ReportType;
            existing.Serious = updated.Serious;
            existing.SeriousnessDeath = updated.SeriousnessDeath;
            existing.SeriousnessLifeThreatening = updated.SeriousnessLifeThreatening;
            existing.SeriousnessHospitalization = updated.SeriousnessHospitalization;
            existing.SeriousnessDisabling = updated.SeriousnessDisabling;
            existing.SeriousnessCongenitalAnomaly = updated.SeriousnessCongenitalAnomaly;
            existing.SeriousnessOther = updated.SeriousnessOther;
            existing.ReceiveDateFormat = updated.ReceiveDateFormat;
            existing.ReceiveDate = updated.ReceiveDate;
            existing.ReceiptDateFormat = updated.ReceiptDateFormat;
            existing.ReceiptDate = updated.ReceiptDate;
            existing.AdditionalDocument = updated.AdditionalDocument;
            existing.FulfillExpediteCriteria = updated.FulfillExpediteCriteria;
            existing.CompanyNumber = updated.CompanyNumber;
            existing.CompanyManufacturer = updated.CompanyManufacturer;
            existing.SeriousnessofADR = updated.SeriousnessofADR;
            existing.ModifiedByUserId = updated.ModifiedByUserId;
            existing.ModifiedDate = updated.ModifiedDate;
        }

        private async Task UpdateRelatedEntitiesAsync(IcsrEntity existing, IcsrEntity updated)
        {
            // Update MessageHeader
            if (updated.MessageHeader != null)
            {
                if (existing.MessageHeader == null)
                {
                    existing.MessageHeader = updated.MessageHeader;
                    existing.MessageHeader.Id = 0; // Let EF assign new ID
                }
                else
                {
                    UpdateMessageHeader(existing.MessageHeader, updated.MessageHeader);
                }
            }

            // Update PrimarySource
            if (updated.PrimarySource != null)
            {
                if (existing.PrimarySource == null)
                {
                    existing.PrimarySource = updated.PrimarySource;
                    existing.PrimarySource.Id = 0;
                }
                else
                {
                    UpdatePrimarySource(existing.PrimarySource, updated.PrimarySource);
                }
            }

            // Update Sender
            if (updated.Sender != null)
            {
                if (existing.Sender == null)
                {
                    existing.Sender = updated.Sender;
                    existing.Sender.Id = 0;
                }
                else
                {
                    UpdateSender(existing.Sender, updated.Sender);
                }
            }

            // Update Receiver
            if (updated.Receiver != null)
            {
                if (existing.Receiver == null)
                {
                    existing.Receiver = updated.Receiver;
                    existing.Receiver.Id = 0;
                }
                else
                {
                    UpdateReceiver(existing.Receiver, updated.Receiver);
                }
            }

            // Update Patient
            if (updated.Patient != null)
            {
                if (existing.Patient == null)
                {
                    existing.Patient = updated.Patient;
                    existing.Patient.Id = 0;
                }
                else
                {
                    UpdatePatient(existing.Patient, updated.Patient);
                }
            }
            await UpdateReactionsAsync(existing, updated);
            await UpdateSummaryAsync(existing, updated);


            // Update ConcomitantMedications
            await UpdateConcomitantMedicationsAsync(existing, updated);

            // Update ConcomitantDiseases
            await UpdateConcomitantDiseasesAsync(existing, updated);
        }

        private async Task UpdateDrugAssociationsAsync(IcsrEntity existingIcsr, List<int> newDrugIds)
        {
            if (existingIcsr.Patient == null)
            {
                return;
            }

            // Load existing drugs explicitly if not already loaded
            await _dbContext.Entry(existingIcsr.Patient)
                .Collection(p => p.Drugs)
                .LoadAsync();

            // Get current drug IDs
            var currentDrugIds = existingIcsr.Patient.Drugs.Select(d => d.Id).ToList();

            // Find drugs to add (new ones not in current)
            var drugsToAddIds = newDrugIds.Except(currentDrugIds).ToList();

            // Find drugs to remove (current ones not in new)
            var drugsToRemoveIds = currentDrugIds.Except(newDrugIds).ToList();

            if (drugsToRemoveIds.Any())
            {
                var drugsToRemove = existingIcsr.Patient.Drugs
                    .Where(d => drugsToRemoveIds.Contains(d.Id))
                    .ToList();

                foreach (var drug in drugsToRemove)
                {
                    existingIcsr.Patient.Drugs.Remove(drug);
                }
            }

            if (drugsToAddIds.Any())
            {
                var drugsToAdd = await _dbContext.Drugs
                    .Where(d => drugsToAddIds.Contains(d.Id) &&
                                (d.CompanyId == existingIcsr.CompanyId || d.CompanyId == null))
                    .ToListAsync();

                foreach (var drug in drugsToAdd)
                {
                    existingIcsr.Patient.Drugs.Add(drug);
                }

                var foundDrugIds = drugsToAdd.Select(d => d.Id).ToList();
                var notFoundDrugIds = drugsToAddIds.Except(foundDrugIds).ToList();
                if (notFoundDrugIds.Any())
                {
                    _logger.LogWarning("Could not associate drugs with IDs {DrugIds} - not found or no access",
                        string.Join(", ", notFoundDrugIds));
                }
            }
        }

        private async Task UpdateConcomitantMedicationsAsync(IcsrEntity existing, IcsrEntity updated)
        {
            // Remove existing medications
            if (existing.ConcomitantMedications?.Any() == true)
            {
                _dbContext.ConcomitantMedications.RemoveRange(existing.ConcomitantMedications);
            }

            // Add new medications
            if (updated.ConcomitantMedications?.Any() == true)
            {
                foreach (var medication in updated.ConcomitantMedications)
                {
                    medication.Id = 0; // Let EF assign new ID
                    medication.IcsrId = existing.Id;
                }
                existing.ConcomitantMedications = updated.ConcomitantMedications;
            }
        }

        private async Task UpdateConcomitantDiseasesAsync(IcsrEntity existing, IcsrEntity updated)
        {
            // Remove existing diseases
            if (existing.ConcomitantDiseases?.Any() == true)
            {
                _dbContext.ConcomitantDiseases.RemoveRange(existing.ConcomitantDiseases);
            }

            // Add new diseases
            if (updated.ConcomitantDiseases?.Any() == true)
            {
                foreach (var disease in updated.ConcomitantDiseases)
                {
                    disease.Id = 0; // Let EF assign new ID
                    disease.IcsrId = existing.Id;
                }
                existing.ConcomitantDiseases = updated.ConcomitantDiseases;
            }
        }

        // Helper methods for updating individual entities
        private void UpdateMessageHeader(MessageHeaderEntity existing, MessageHeaderEntity updated)
        {
            existing.MessageType = updated.MessageType;
            existing.MessageFormatVersion = updated.MessageFormatVersion;
            existing.MessageFormatRelease = updated.MessageFormatRelease;
            existing.MessageNumber = updated.MessageNumber;
            existing.MessageSenderIdentifier = updated.MessageSenderIdentifier;
            existing.MessageReceiverIdentifier = updated.MessageReceiverIdentifier;
            existing.MessageDateFormat = updated.MessageDateFormat;
            existing.MessageDate = updated.MessageDate;
            existing.ModifiedDate = DateTime.UtcNow;
        }

        private void UpdatePrimarySource(PrimarySourceEntity existing, PrimarySourceEntity updated)
        {
            existing.ReporterFamilyName = updated.ReporterFamilyName;
            existing.ReporterOrganization = updated.ReporterOrganization;
            existing.ReporterState = updated.ReporterState;
            existing.ReporterPostCode = updated.ReporterPostCode;
            existing.ReporterCountry = updated.ReporterCountry;
            existing.Qualification = updated.Qualification;
            existing.ModifiedDate = DateTime.UtcNow;
        }

        private void UpdateSender(SenderEntity existing, SenderEntity updated)
        {
            existing.SenderType = updated.SenderType;
            existing.SenderOrganization = updated.SenderOrganization;
            existing.SenderCountryCode = updated.SenderCountryCode;
            existing.SenderDepartment = updated.SenderDepartment;
            existing.SenderTitle = updated.SenderTitle;
            existing.SenderGiveName = updated.SenderGiveName;
            existing.SenderStreetAddress = updated.SenderStreetAddress;
            existing.SenderTel = updated.SenderTel;
            existing.SenderEmailAddress = updated.SenderEmailAddress;
            existing.ModifiedDate = DateTime.UtcNow;
        }

        private void UpdateReceiver(ReceiverEntity existing, ReceiverEntity updated)
        {
            existing.ReceiverType = updated.ReceiverType;
            existing.ReceiverOrganization = updated.ReceiverOrganization;
            existing.ReceiverCountryCode = updated.ReceiverCountryCode;
            existing.ModifiedDate = DateTime.UtcNow;
        }

        private async void UpdatePatient(PatientEntity existing, PatientEntity updated)
        {
            existing.CaseId= updated.CaseId;
            existing.PatientInitial = updated.PatientInitial;
            existing.PatientGpMedicalRecordNumber = updated.PatientGpMedicalRecordNumber;
            existing.PatientSpecialistRecordNumber = updated.PatientSpecialistRecordNumber;
            existing.PatientHospitalRecordNumber = updated.PatientHospitalRecordNumber;
            existing.PatientInvestigationNumber = updated.PatientInvestigationNumber;
            existing.PatientBirthDateFormat = updated.PatientBirthDateFormat;
            existing.PatientBirthDate = updated.PatientBirthDate;
            existing.PatientOnsetAge = updated.PatientOnsetAge;
            existing.PatientOnsetAgeUnit = updated.PatientOnsetAgeUnit;
            existing.GestationPeriod = updated.GestationPeriod;
            existing.GestationPeriodUnit = updated.GestationPeriodUnit;
            existing.PatientAgeGroup = updated.PatientAgeGroup;
            existing.PatientWeight = updated.PatientWeight;
            existing.PatientHeight = updated.PatientHeight;
            existing.PatientSex = updated.PatientSex;
            existing.LastMenstrualDateFormat = updated.LastMenstrualDateFormat;
            existing.PatientLastMenstrualDate = updated.PatientLastMenstrualDate;
            existing.ModifiedDate = DateTime.UtcNow;

        }
        private async Task UpdateReactionsAsync(IcsrEntity existing, IcsrEntity updated)
        {
            if (existing.Patient == null || updated.Patient?.Reactions == null) return;

            // Remove all existing reactions
            if (existing.Patient.Reactions != null)
            {
                _dbContext.Reactions.RemoveRange(existing.Patient.Reactions);
                existing.Patient.Reactions.Clear();
            }

            // Add new reactions
            foreach (var newReaction in updated.Patient.Reactions)
            {
                newReaction.Id = 0; // Ensure EF creates a new entry
                newReaction.PatientId = existing.Patient.Id;
                existing.Patient.Reactions.Add(newReaction);
            }
        }

        //private async Task UpdateReactionsAsync(IcsrEntity existing, IcsrEntity updated)
        //{
        //    if (existing.Patient == null || updated.Patient?.Reactions == null) return;

        //    var existingReactions = existing.Patient.Reactions?.ToList() ?? new List<ReactionEntity>();
        //    var updatedReactions = updated.Patient.Reactions.ToList();

        //    // Update existing reactions
        //    foreach (var existingReaction in existingReactions)
        //    {
        //        var updatedReaction = updatedReactions.FirstOrDefault(r => r.Id == existingReaction.Id);
        //        if (updatedReaction != null)
        //        {
        //            UpdateReaction(existingReaction, updatedReaction);
        //            updatedReactions.Remove(updatedReaction); // Remove from list to track what's left
        //        }
        //        else
        //        {
        //            // Reaction no longer exists in updated data, remove it
        //            _dbContext.Reactions.Remove(existingReaction);
        //        }
        //    }

        //    // Add new reactions (remaining items in updatedReactions)
        //    foreach (var newReaction in updatedReactions)
        //    {
        //        newReaction.Id = 0; // Let EF assign new ID
        //        newReaction.PatientId = existing.Patient.Id;
        //        existing.Patient.Reactions.Add(newReaction);
        //    }
        //}

        // ADD THIS HELPER METHOD
        private void UpdateReaction(ReactionEntity existing, ReactionEntity updated)
        {
            existing.PrimarySourceReaction = updated.PrimarySourceReaction;
            existing.ReactionMeddraVersionLLT = updated.ReactionMeddraVersionLLT;
            existing.ReactionMeddraLLT = updated.ReactionMeddraLLT;
            existing.ReactionMeddraVersionPT = updated.ReactionMeddraVersionPT;
            existing.ReactionMeddraPT = updated.ReactionMeddraPT;
            existing.TermHighlighted = updated.TermHighlighted;
            existing.ReactionStartDateFormat = updated.ReactionStartDateFormat;
            existing.ReactionStartDate = updated.ReactionStartDate;
            existing.ReactionEndDateFormat = updated.ReactionEndDateFormat;
            existing.ReactionEndDate = updated.ReactionEndDate;
            existing.ReactionDurationUnit = updated.ReactionDurationUnit;
            existing.ReactionOutcome = updated.ReactionOutcome;
            existing.ModifiedDate = DateTime.UtcNow;
        }

        private async Task UpdateSummaryAsync(IcsrEntity existing, IcsrEntity updated)
        {
            if (existing.Patient == null) return;

            // Update Summary
            if (updated.Patient?.Summary != null)
            {
                if (existing.Patient.Summary == null)
                {
                    existing.Patient.Summary = updated.Patient.Summary;
                    existing.Patient.Summary.Id = 0; // Let EF assign new ID
                    existing.Patient.Summary.PatientId = existing.Patient.Id;
                }
                else
                {
                    UpdateSummary(existing.Patient.Summary, updated.Patient.Summary);
                }
            }
            else if (existing.Patient.Summary != null)
            {
                // Summary was removed in the update
                _dbContext.Summaries.Remove(existing.Patient.Summary);
                existing.Patient.Summary = null;
            }
        }

        // ADD THIS HELPER METHOD
        private void UpdateSummary(SummaryEntity existing, SummaryEntity updated)
        {
            existing.NarrativeIncludeClinical = updated.NarrativeIncludeClinical;
            existing.ModifiedDate = DateTime.UtcNow;
        }
    }
}