using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.IServicesBL;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.IRepository;
using DataAccess.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using iText.Layout;
using iText.Layout.Borders;
using iText.Kernel.Geom;

namespace BusinessLogic.Services
{

    public class IcsrService : IIcsrService
    {
        private readonly IIcsrRepository _icsrRepository;
        private readonly IDrugRepository _dugrRepository;
        private readonly ILogger<IcsrService> _logger;


        public IcsrService(IIcsrRepository icsrRepository, IDrugRepository drugRepository, ILogger<IcsrService> logger)
        {
            _icsrRepository = icsrRepository ?? throw new ArgumentNullException(nameof(icsrRepository));
            _dugrRepository = drugRepository ?? throw new ArgumentNullException(nameof(drugRepository));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IchIcsrXMLModel> GetIcsrByIdAsync(int Id, int? companyId)
        {
            try
            {
                _logger.LogInformation("Getting ICSR with safety report ID: {SafetyReportId}", Id);
                var icsrEntity = await _icsrRepository.GetByIdAsync(Id, companyId);

                if (icsrEntity == null)
                {
                    _logger.LogWarning("ICSR with safety report ID: {SafetyReportId} not found", Id);
                    return null;
                }

                var model = MapToModel(icsrEntity);
                return CleanNullValues(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving ICSR with safety report ID: {SafetyReportId}", Id);
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAllIcsrIdsAsync()
        {
            try
            {
                _logger.LogInformation("Getting all ICSR IDs");
                return await _icsrRepository.GetAllIdsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all ICSR IDs");
                throw;
            }
        }

        public async Task<bool> UpdateIcsrAsync(IchIcsrXMLModel updatedIcsr, int userId, int? companyId)
        {
            try
            {
                if (updatedIcsr == null || updatedIcsr.SafetyReport == null)
                {
                    _logger.LogWarning("Attempted to update null ICSR or ICSR with null safety report");
                    return false;
                }

                _logger.LogInformation("Updating ICSR with safety report ID: {SafetyReportId}", updatedIcsr.SafetyReport.SafetyReportId);

                var icsrEntity = MapToEntity(updatedIcsr);
                return await _icsrRepository.UpdateAsync(icsrEntity, userId, companyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ICSR with safety report ID: {SafetyReportId}", updatedIcsr?.SafetyReport?.SafetyReportId ?? "unknown");
                throw;
            }
        }

        public async Task<bool> DeleteIcsrAsync(int safetyReportId, int userId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Deleting ICSR with safety report ID: {SafetyReportId}", safetyReportId);
                return await _icsrRepository.DeleteAsync(safetyReportId, userId, companyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting ICSR with safety report ID: {SafetyReportId}", safetyReportId);
                throw;
            }
        }
        public async Task<PaginatedResponse<IchIcsrXMLModel>> GetPaginatedIcsrsAsync(int pageNumber, int pageSize, int? companyId)
        {
            try
            {
                _logger.LogInformation("Getting paginated ICSR data: Page {PageNumber}, Size {PageSize}", pageNumber, pageSize);

                var totalCount = await _icsrRepository.GetTotalCountAsync(companyId);
                var icsrs = await _icsrRepository.GetPaginatedAsync(pageNumber, pageSize, companyId);

                var mappedIcsrs = icsrs.Select(MapToModel).ToList();

                return new PaginatedResponse<IchIcsrXMLModel>
                {
                    Items = mappedIcsrs,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving paginated ICSR data");
                throw;
            }
        }
        public Dictionary<string, Dictionary<string, int>> GetAllEnums()
        {
            var result = new Dictionary<string, Dictionary<string, int>>();

            var enumTypes = typeof(MessageType).Namespace != null
                ? Assembly.GetAssembly(typeof(MessageType))!
                    .GetTypes()
                    .Where(t => t.IsEnum && t.Namespace == typeof(MessageType).Namespace)
                : Enumerable.Empty<Type>();

            foreach (var enumType in enumTypes)
            {
                var values = Enum.GetValues(enumType)
                    .Cast<Enum>()
                    .ToDictionary(e => e.ToString(), e => Convert.ToInt32(e));

                result.Add(enumType.Name, values);
            }

            return result;
        }
        private IcsrEntity MapToEntity(IchIcsrXMLModel model)
        {
            var entity = new IcsrEntity
            {
                // Base properties from IchIcsrXMLModel
                Id = model.Id == null ? 0 : model.Id,
                Language = model.Language,
                SafetyReportId = model.SafetyReport.SafetyReportId,
                SafetyReportVersion = model.SafetyReport.SafetyReportVersion,
                PrimarySourceCountry = model.SafetyReport.PrimarySourceCountry,
                OccurCountry = model.SafetyReport.OccurCountry,
                TransmissionDateFormat = model.SafetyReport.TransmissionDateFormat,
                TransmissionDate = model.SafetyReport.TransmissionDate,
                ReportType = model.SafetyReport.ReportType,
                Serious = model.SafetyReport.Serious,
                SeriousnessDeath = model.SafetyReport.SeriousnessDeath,
                SeriousnessLifeThreatening = model.SafetyReport.SeriousnessLifeThreatening,
                SeriousnessHospitalization = model.SafetyReport.SeriousnessHospitalization,
                SeriousnessDisabling = model.SafetyReport.SeriousnessDisabling,
                SeriousnessCongenitalAnomaly = model.SafetyReport.SeriousnessCongenitalAnomaly,
                SeriousnessOther = model.SafetyReport.SeriousnessOther,
                ReceiveDateFormat = model.SafetyReport.ReceiveDateFormat,
                ReceiveDate = model.SafetyReport.ReceiveDate,
                ReceiptDateFormat = model.SafetyReport.ReceiptDateFormat,
                ReceiptDate = model.SafetyReport.ReceiptDate,
                AdditionalDocument = model.SafetyReport.AdditionalDocument,
                FulfillExpediteCriteria = model.SafetyReport.FulfillExpediteCriteria,
                CompanyNumber = model.SafetyReport.CompanyNumber,
                CompanyManufacturer = model.SafetyReport.CompanyManufacturer,
                SeriousnessofADR = model.SafetyReport.SeriousnessofADR,

                // Map the message header
                MessageHeader = new MessageHeaderEntity
                {
                    MessageType = model.MessageHeader.MessageType,
                    MessageFormatVersion = model.MessageHeader.MessageFormatVersion,
                    MessageFormatRelease = model.MessageHeader.MessageFormatRelease,
                    MessageNumber = model.MessageHeader.MessageNumber,
                    MessageSenderIdentifier = model.MessageHeader.MessageSenderIdentifier,
                    MessageReceiverIdentifier = model.MessageHeader.MessageReceiverIdentifier,
                    MessageDateFormat = model.MessageHeader.MessageDateFormat,
                    MessageDate = model.MessageHeader.MessageDate
                }
            };

            // Map primary source if exists
            if (model.SafetyReport.PrimarySource != null)
            {
                entity.PrimarySource = new PrimarySourceEntity
                {
                    ReporterFamilyName = model.SafetyReport.PrimarySource.ReporterFamilyName,
                    ReporterOrganization = model.SafetyReport.PrimarySource.ReporterOrganization,
                    ReporterState = model.SafetyReport.PrimarySource.ReporterState,
                    ReporterPostCode = model.SafetyReport.PrimarySource.ReporterPostCode,
                    ReporterCountry = model.SafetyReport.PrimarySource.ReporterCountry,
                    Qualification = model.SafetyReport.PrimarySource.Qualification
                };
            }

            // Map sender if exists
            if (model.SafetyReport.Sender != null)
            {
                entity.Sender = new SenderEntity
                {
                    SenderType = model.SafetyReport.Sender.SenderType,
                    SenderOrganization = model.SafetyReport.Sender.SenderOrganization,
                    SenderCountryCode = model.SafetyReport.Sender.SenderCountryCode,
                    SenderDepartment = model.SafetyReport.Sender.SenderDepartment,
                    SenderTitle = model.SafetyReport.Sender.SenderTitle,
                    SenderGiveName = model.SafetyReport.Sender.SenderGiveName,
                    SenderStreetAddress = model.SafetyReport.Sender.SenderStreetAddress,
                    SenderTel = model.SafetyReport.Sender.SenderTel,
                    SenderEmailAddress = model.SafetyReport.Sender.SenderEmailAddress
                };
            }

            // Map receiver if exists
            if (model.SafetyReport.Receiver != null)
            {
                entity.Receiver = new ReceiverEntity
                {
                    ReceiverType = model.SafetyReport.Receiver.ReceiverType,
                    ReceiverOrganization = model.SafetyReport.Receiver.ReceiverOrganization,
                    ReceiverCountryCode = model.SafetyReport.Receiver.ReceiverCountryCode
                };
            }

            // Map patient if exists
            if (model.SafetyReport.Patient != null)
            {
                entity.Patient = new PatientEntity
                {
                    CaseId = model.SafetyReport.Patient.CaseId,
                    PatientInitial = model.SafetyReport.Patient.PatientInitial,
                    PatientGpMedicalRecordNumber = model.SafetyReport.Patient.PatientGpMedicalRecordNumber,
                    PatientSpecialistRecordNumber = model.SafetyReport.Patient.PatientSpecialistRecordNumber,
                    PatientHospitalRecordNumber = model.SafetyReport.Patient.PatientHospitalRecordNumber,
                    PatientInvestigationNumber = model.SafetyReport.Patient.PatientInvestigationNumber,
                    PatientBirthDateFormat = model.SafetyReport.Patient.PatientBirthDateFormat,
                    PatientBirthDate = model.SafetyReport.Patient.PatientBirthDate,
                    PatientOnsetAge = model.SafetyReport.Patient.PatientOnsetAge,
                    PatientOnsetAgeUnit = model.SafetyReport.Patient.PatientOnsetAgeUnit,
                    GestationPeriod = model.SafetyReport.Patient.GestationPeriod,
                    GestationPeriodUnit = model.SafetyReport.Patient.GestationPeriodUnit,
                    PatientAgeGroup = model.SafetyReport.Patient.PatientAgeGroup,
                    PatientWeight = model.SafetyReport.Patient.PatientWeight,
                    PatientHeight = model.SafetyReport.Patient.PatientHeight,
                    PatientSex = model.SafetyReport.Patient.PatientSex,
                    LastMenstrualDateFormat = model.SafetyReport.Patient.LastMenstrualDateFormat,
                    PatientLastMenstrualDate = model.SafetyReport.Patient.PatientLastMenstrualDate,
                    Reactions = new List<ReactionEntity>(),
                    Drugs = new List<DrugEntity>()
                };

                // Map reactions if they exist
                if (model.SafetyReport.Patient.Reactions != null)
                {
                    foreach (var reaction in model.SafetyReport.Patient.Reactions)
                    {
                        entity.Patient.Reactions.Add(new ReactionEntity
                        {
                            PrimarySourceReaction = reaction.PrimarySourceReaction,
                            ReactionMeddraVersionLLT = reaction.ReactionMeddraVersionLLT,
                            ReactionMeddraLLT = reaction.ReactionMeddraLLT,
                            ReactionMeddraVersionPT = reaction.ReactionMeddraVersionPT,
                            ReactionMeddraPT = reaction.ReactionMeddraPT,
                            TermHighlighted = reaction.TermHighlighted,
                            ReactionStartDateFormat = reaction.ReactionStartDateFormat,
                            ReactionStartDate = reaction.ReactionStartDate,
                            ReactionEndDateFormat = reaction.ReactionEndDateFormat,
                            ReactionEndDate = reaction.ReactionEndDate,
                            ReactionDurationUnit = reaction.ReactionDurationUnit,
                            ReactionOutcome = reaction.ReactionOutcome
                        });
                    }
                }

                // Map drugs if they exist
                if (model.SafetyReport.Patient.Drugs != null)
                {
                    foreach (var drug in model.SafetyReport.Patient.Drugs)
                    {
                        var drugEntity = new DrugEntity
                        {
                            DrugCharacterization = drug.DrugCharacterization,
                            MedicinalProduct = drug.MedicinalProduct,
                            TradeName = drug.TradeName,
                            //  ActiveSubstanceName = drug.ActiveSubstanceName,
                            ObtainDrugCountry = drug.ObtainDrugCountry,
                            DrugAdministrationRoute = drug.DrugAdministrationRoute,
                            DrugAuthorizationCountry = drug.DrugAuthorizationCountry,
                            DrugStartDateFormat = drug.DrugStartDateFormat,
                            DrugStartDate = drug.DrugStartDate,
                            DrugEndDateFormat = drug.DrugEndDateFormat,
                            DrugEndDate = drug.DrugEndDate,
                            DrugIndicationMeddraVersion = drug.DrugIndicationMeddraVersion,
                            DrugIndication = drug.DrugIndication,
                            ActionDrug = drug.ActionDrug,
                            //   DrugRecurReadministration = drug.DrugRecurReadministration ?? null,
                            DrugReactionRelatedness = new List<DrugReactionRelatednessEntity>()
                        };

                        // Map active substance if exists
                        if (drug.ActiveSubstance != null)
                        {
                            drugEntity.ActiveSubstance = new ActiveSubstanceEntity
                            {
                                ActiveSubstanceName = drug.ActiveSubstance.ActiveSubstanceName
                            };
                        }

                        // Map drug reaction relatedness if exists
                        if (drug.DrugReactionRelatedness != null)
                        {
                            foreach (var relatedness in drug.DrugReactionRelatedness)
                            {
                                drugEntity.DrugReactionRelatedness.Add(new DrugReactionRelatednessEntity
                                {
                                    DrugReactionAssesMeddraVersion = relatedness.DrugReactionAssesMeddraVersion,
                                    DrugReactionAsses = relatedness.DrugReactionAsses,
                                    DrugAssessmentSource = relatedness.DrugAssessmentSource,
                                    DrugAssessmentMethod = relatedness.DrugAssessmentMethod,
                                    DrugResult = relatedness.DrugResult
                                });
                            }
                        }

                        entity.Patient.Drugs.Add(drugEntity);
                    }
                }

                // Map summary if exists
                if (model.SafetyReport.Patient.Summary != null)
                {
                    entity.Patient.Summary = new SummaryEntity
                    {
                        NarrativeIncludeClinical = model.SafetyReport.Patient.Summary.NarrativeIncludeClinical
                    };
                }
            }

            // Map concomitant medications if they exist
            if (model.ConcomitantMedications != null)
            {
                entity.ConcomitantMedications = new List<ConcomitantMedicationEntity>();
                foreach (var medication in model.ConcomitantMedications)
                {
                    entity.ConcomitantMedications.Add(new ConcomitantMedicationEntity
                    {
                        MedicationName = medication.MedicationName,
                        ActiveIngredient = medication.ActiveIngredient,
                        MedicationCountry = medication.MedicationCountry,
                        StartDateFormat = medication.StartDateFormat,
                        StartDate = medication.StartDate,
                        EndDateFormat = medication.EndDateFormat,
                        EndDate = medication.EndDate
                    });
                }
            }

            // Map concomitant diseases if they exist
            if (model.ConcomitantDiseases != null)
            {
                entity.ConcomitantDiseases = new List<ConcomitantDiseaseEntity>();
                foreach (var disease in model.ConcomitantDiseases)
                {
                    entity.ConcomitantDiseases.Add(new ConcomitantDiseaseEntity
                    {
                        DiseaseName = disease.DiseaseName,
                        DiseaseType = disease.DiseaseType,
                        StartDateFormat = disease.StartDateFormat,
                        StartDate = disease.StartDate,
                        Continuing = disease.Continuing,
                        EndDateFormat = disease.EndDateFormat,
                        EndDate = disease.EndDate
                    });
                }
            }

            return entity;
        }
        private IcsrEntity MapToEntity(IchIcsrXMLModelWithDrugIds model)
        {
            var entity = new IcsrEntity
            {
                // Base properties from IchIcsrXMLModel
                Id = model.Id == null ? 0 : model.Id,
                Language = model.Language,
                SafetyReportId = model.SafetyReport.SafetyReportId,
                SafetyReportVersion = model.SafetyReport.SafetyReportVersion,
                PrimarySourceCountry = model.SafetyReport.PrimarySourceCountry,
                OccurCountry = model.SafetyReport.OccurCountry,
                TransmissionDateFormat = model.SafetyReport.TransmissionDateFormat,
                TransmissionDate = model.SafetyReport.TransmissionDate,
                ReportType = model.SafetyReport.ReportType,
                Serious = model.SafetyReport.Serious,
                SeriousnessDeath = model.SafetyReport.SeriousnessDeath,
                SeriousnessLifeThreatening = model.SafetyReport.SeriousnessLifeThreatening,
                SeriousnessHospitalization = model.SafetyReport.SeriousnessHospitalization,
                SeriousnessDisabling = model.SafetyReport.SeriousnessDisabling,
                SeriousnessCongenitalAnomaly = model.SafetyReport.SeriousnessCongenitalAnomaly,
                SeriousnessOther = model.SafetyReport.SeriousnessOther,
                ReceiveDateFormat = model.SafetyReport.ReceiveDateFormat,
                ReceiveDate = model.SafetyReport.ReceiveDate,
                ReceiptDateFormat = model.SafetyReport.ReceiptDateFormat,
                ReceiptDate = model.SafetyReport.ReceiptDate,
                AdditionalDocument = model.SafetyReport.AdditionalDocument,
                FulfillExpediteCriteria = model.SafetyReport.FulfillExpediteCriteria,
                CompanyNumber = model.SafetyReport.CompanyNumber,
                SeriousnessofADR = model.SafetyReport.SeriousnessofADR,
                CompanyManufacturer = model.SafetyReport.CompanyManufacturer,

                // Map the message header
                MessageHeader = new MessageHeaderEntity
                {
                    MessageType = model.MessageHeader.MessageType,
                    MessageFormatVersion = model.MessageHeader.MessageFormatVersion,
                    MessageFormatRelease = model.MessageHeader.MessageFormatRelease,
                    MessageNumber = model.MessageHeader.MessageNumber,
                    MessageSenderIdentifier = model.MessageHeader.MessageSenderIdentifier,
                    MessageReceiverIdentifier = model.MessageHeader.MessageReceiverIdentifier,
                    MessageDateFormat = model.MessageHeader.MessageDateFormat,
                    MessageDate = model.MessageHeader.MessageDate
                }
            };

            // Map primary source if exists
            if (model.SafetyReport.PrimarySource != null)
            {
                entity.PrimarySource = new PrimarySourceEntity
                {
                    ReporterFamilyName = model.SafetyReport.PrimarySource.ReporterFamilyName,
                    ReporterOrganization = model.SafetyReport.PrimarySource.ReporterOrganization,
                    ReporterState = model.SafetyReport.PrimarySource.ReporterState,
                    ReporterPostCode = model.SafetyReport.PrimarySource.ReporterPostCode,
                    ReporterCountry = model.SafetyReport.PrimarySource.ReporterCountry,
                    Qualification = model.SafetyReport.PrimarySource.Qualification
                };
            }

            // Map sender if exists
            if (model.SafetyReport.Sender != null)
            {
                entity.Sender = new SenderEntity
                {
                    SenderType = model.SafetyReport.Sender.SenderType,
                    SenderOrganization = model.SafetyReport.Sender.SenderOrganization,
                    SenderCountryCode = model.SafetyReport.Sender.SenderCountryCode,
                    SenderDepartment = model.SafetyReport.Sender.SenderDepartment,
                    SenderTitle = model.SafetyReport.Sender.SenderTitle,
                    SenderGiveName = model.SafetyReport.Sender.SenderGiveName,
                    SenderStreetAddress = model.SafetyReport.Sender.SenderStreetAddress,
                    SenderTel = model.SafetyReport.Sender.SenderTel,
                    SenderEmailAddress = model.SafetyReport.Sender.SenderEmailAddress
                };
            }

            // Map receiver if exists
            if (model.SafetyReport.Receiver != null)
            {
                entity.Receiver = new ReceiverEntity
                {
                    ReceiverType = model.SafetyReport.Receiver.ReceiverType,
                    ReceiverOrganization = model.SafetyReport.Receiver.ReceiverOrganization,
                    ReceiverCountryCode = model.SafetyReport.Receiver.ReceiverCountryCode
                };
            }

            // Map patient if exists
            if (model.SafetyReport.Patient != null)
            {
                entity.Patient = new PatientEntity
                {
                    CaseId = model.SafetyReport.Patient.CaseId,
                    PatientInitial = model.SafetyReport.Patient.PatientInitial,
                    PatientGpMedicalRecordNumber = model.SafetyReport.Patient.PatientGpMedicalRecordNumber,
                    PatientSpecialistRecordNumber = model.SafetyReport.Patient.PatientSpecialistRecordNumber,
                    PatientHospitalRecordNumber = model.SafetyReport.Patient.PatientHospitalRecordNumber,
                    PatientInvestigationNumber = model.SafetyReport.Patient.PatientInvestigationNumber,
                    PatientBirthDateFormat = model.SafetyReport.Patient.PatientBirthDateFormat,
                    PatientBirthDate = model.SafetyReport.Patient.PatientBirthDate,
                    PatientOnsetAge = model.SafetyReport.Patient.PatientOnsetAge,
                    PatientOnsetAgeUnit = model.SafetyReport.Patient.PatientOnsetAgeUnit,
                    GestationPeriod = model.SafetyReport.Patient.GestationPeriod,
                    GestationPeriodUnit = model.SafetyReport.Patient.GestationPeriodUnit,
                    PatientAgeGroup = model.SafetyReport.Patient.PatientAgeGroup,
                    PatientWeight = model.SafetyReport.Patient.PatientWeight,
                    PatientHeight = model.SafetyReport.Patient.PatientHeight,
                    PatientSex = model.SafetyReport.Patient.PatientSex,
                    LastMenstrualDateFormat = model.SafetyReport.Patient.LastMenstrualDateFormat,
                    PatientLastMenstrualDate = model.SafetyReport.Patient.PatientLastMenstrualDate,
                    Reactions = new List<ReactionEntity>(),
                    Drugs = new List<DrugEntity>()
                };

                // Map reactions if they exist
                if (model.SafetyReport.Patient.Reactions != null)
                {
                    foreach (var reaction in model.SafetyReport.Patient.Reactions)
                    {
                        entity.Patient.Reactions.Add(new ReactionEntity
                        {
                            PrimarySourceReaction = reaction.PrimarySourceReaction,
                            ReactionMeddraVersionLLT = reaction.ReactionMeddraVersionLLT,
                            ReactionMeddraLLT = reaction.ReactionMeddraLLT,
                            ReactionMeddraVersionPT = reaction.ReactionMeddraVersionPT,
                            ReactionMeddraPT = reaction.ReactionMeddraPT,
                            TermHighlighted = reaction.TermHighlighted,
                            ReactionStartDateFormat = reaction.ReactionStartDateFormat,
                            ReactionStartDate = reaction.ReactionStartDate,
                            ReactionEndDateFormat = reaction.ReactionEndDateFormat,
                            ReactionEndDate = reaction.ReactionEndDate,
                            ReactionDurationUnit = reaction.ReactionDurationUnit,
                            ReactionOutcome = reaction.ReactionOutcome
                        });
                    }
                }
                // Map summary if exists
                if (model.SafetyReport.Patient.Summary != null)
                {
                    entity.Patient.Summary = new SummaryEntity
                    {
                        NarrativeIncludeClinical = model.SafetyReport.Patient.Summary.NarrativeIncludeClinical
                    };
                }
            }

            // Map concomitant medications if they exist
            if (model.ConcomitantMedications != null)
            {
                entity.ConcomitantMedications = new List<ConcomitantMedicationEntity>();
                foreach (var medication in model.ConcomitantMedications)
                {
                    entity.ConcomitantMedications.Add(new ConcomitantMedicationEntity
                    {
                        MedicationName = medication.MedicationName,
                        ActiveIngredient = medication.ActiveIngredient,
                        MedicationCountry = medication.MedicationCountry,
                        StartDateFormat = medication.StartDateFormat,
                        StartDate = medication.StartDate,
                        EndDateFormat = medication.EndDateFormat,
                        EndDate = medication.EndDate
                    });
                }
            }

            // Map concomitant diseases if they exist
            if (model.ConcomitantDiseases != null)
            {
                entity.ConcomitantDiseases = new List<ConcomitantDiseaseEntity>();
                foreach (var disease in model.ConcomitantDiseases)
                {
                    entity.ConcomitantDiseases.Add(new ConcomitantDiseaseEntity
                    {
                        DiseaseName = disease.DiseaseName,
                        DiseaseType = disease.DiseaseType,
                        StartDateFormat = disease.StartDateFormat,
                        StartDate = disease.StartDate,
                        Continuing = disease.Continuing,
                        EndDateFormat = disease.EndDateFormat,
                        EndDate = disease.EndDate
                    });
                }
            }

            return entity;
        }

        private IchIcsrXMLModel MapToModel(IcsrEntity entity)
        {
            var model = new IchIcsrXMLModel
            {
                Language = entity.Language,
                Id = entity.Id,
                MessageHeader = new IchIcsrMessageHeader
                {
                    MessageType = entity.MessageHeader?.MessageType,
                    MessageFormatVersion = entity.MessageHeader?.MessageFormatVersion,
                    MessageFormatRelease = entity.MessageHeader?.MessageFormatRelease,
                    MessageNumber = entity.MessageHeader?.MessageNumber,
                    MessageSenderIdentifier = entity.MessageHeader?.MessageSenderIdentifier,
                    MessageReceiverIdentifier = entity.MessageHeader?.MessageReceiverIdentifier,
                    MessageDateFormat = entity.MessageHeader?.MessageDateFormat ?? 0,
                    MessageDate = entity.MessageHeader?.MessageDate
                },
                SafetyReport = new SafetyReport
                {
                    SafetyReportId = entity.SafetyReportId,
                    SafetyReportVersion = entity.SafetyReportVersion ?? 0,
                    PrimarySourceCountry = entity.PrimarySourceCountry,
                    OccurCountry = entity.OccurCountry,
                    TransmissionDateFormat = entity.TransmissionDateFormat ?? 0,
                    TransmissionDate = entity.TransmissionDate,
                    ReportType = entity.ReportType ?? 0,
                    Serious = entity.Serious ?? 0,
                    SeriousnessDeath = entity.SeriousnessDeath ?? null,
                    SeriousnessLifeThreatening = entity.SeriousnessLifeThreatening ?? null,
                    SeriousnessHospitalization = entity.SeriousnessHospitalization ?? null,
                    SeriousnessDisabling = entity.SeriousnessDisabling ?? null,
                    SeriousnessCongenitalAnomaly = entity.SeriousnessCongenitalAnomaly ?? null,
                    SeriousnessOther = entity.SeriousnessOther ?? null,
                    ReceiveDateFormat = entity.ReceiveDateFormat ?? 0,
                    ReceiveDate = entity.ReceiveDate,
                    ReceiptDateFormat = entity.ReceiptDateFormat ?? 0,
                    ReceiptDate = entity.ReceiptDate,
                    AdditionalDocument = entity.AdditionalDocument ?? 0,
                    FulfillExpediteCriteria = entity.FulfillExpediteCriteria ?? 0,
                    CompanyNumber = entity.CompanyNumber,
                    CompanyManufacturer = entity.CompanyManufacturer,
                    SeriousnessofADR = entity.SeriousnessofADR
                },
                ConcomitantMedications = new List<ConcomitantMedication>(),
                ConcomitantDiseases = new List<ConcomitantDisease>()
            };

            // Map primary source if exists
            if (entity.PrimarySource != null)
            {
                model.SafetyReport.PrimarySource = new PrimarySource
                {
                    ReporterFamilyName = entity.PrimarySource.ReporterFamilyName,
                    ReporterOrganization = entity.PrimarySource.ReporterOrganization,
                    ReporterState = entity.PrimarySource.ReporterState,
                    ReporterPostCode = entity.PrimarySource.ReporterPostCode,
                    ReporterCountry = entity.PrimarySource.ReporterCountry,
                    Qualification = entity.PrimarySource.Qualification
                };
            }

            // Map sender if exists
            if (entity.Sender != null)
            {
                model.SafetyReport.Sender = new Sender
                {
                    SenderType = entity.Sender.SenderType,
                    SenderOrganization = entity.Sender.SenderOrganization,
                    SenderCountryCode = entity.Sender.SenderCountryCode,
                    SenderDepartment = entity.Sender.SenderDepartment,
                    SenderTitle = entity.Sender.SenderTitle,
                    SenderGiveName = entity.Sender.SenderGiveName,
                    SenderStreetAddress = entity.Sender.SenderStreetAddress,
                    SenderTel = entity.Sender.SenderTel,
                    SenderEmailAddress = entity.Sender.SenderEmailAddress
                };
            }

            // Map receiver if exists
            if (entity.Receiver != null)
            {
                model.SafetyReport.Receiver = new Receiver
                {
                    ReceiverType = entity.Receiver.ReceiverType,
                    ReceiverOrganization = entity.Receiver.ReceiverOrganization,
                    ReceiverCountryCode = entity.Receiver.ReceiverCountryCode
                };
            }

            // Map patient if exists
            if (entity.Patient != null)
            {
                model.SafetyReport.Patient = new Patient
                {
                    CaseId = entity.Patient.CaseId,
                    PatientInitial = entity.Patient.PatientInitial,
                    PatientGpMedicalRecordNumber = entity.Patient.PatientGpMedicalRecordNumber,
                    PatientSpecialistRecordNumber = entity.Patient.PatientSpecialistRecordNumber,
                    PatientHospitalRecordNumber = entity.Patient.PatientHospitalRecordNumber,
                    PatientInvestigationNumber = entity.Patient.PatientInvestigationNumber,
                    PatientBirthDateFormat = entity.Patient.PatientBirthDateFormat,
                    PatientBirthDate = entity.Patient.PatientBirthDate,
                    PatientOnsetAge = entity.Patient.PatientOnsetAge,
                    PatientOnsetAgeUnit = entity.Patient.PatientOnsetAgeUnit,
                    GestationPeriod = entity.Patient.GestationPeriod,
                    GestationPeriodUnit = entity.Patient.GestationPeriodUnit,
                    PatientAgeGroup = entity.Patient.PatientAgeGroup,
                    PatientWeight = entity.Patient.PatientWeight,
                    PatientHeight = entity.Patient.PatientHeight,
                    PatientSex = entity.Patient.PatientSex,
                    LastMenstrualDateFormat = entity.Patient.LastMenstrualDateFormat,
                    PatientLastMenstrualDate = entity.Patient.PatientLastMenstrualDate,
                    Reactions = new List<Reaction>(),
                    Drugs = new List<Drug>()
                };

                // Map reactions if they exist
                if (entity.Patient.Reactions != null && entity.Patient.Reactions.Any())
                {
                    foreach (var reaction in entity.Patient.Reactions)
                    {
                        model.SafetyReport.Patient.Reactions.Add(new Reaction
                        {
                            PrimarySourceReaction = reaction.PrimarySourceReaction,
                            ReactionMeddraVersionLLT = reaction.ReactionMeddraVersionLLT,
                            ReactionMeddraLLT = reaction.ReactionMeddraLLT,
                            ReactionMeddraVersionPT = reaction.ReactionMeddraVersionPT,
                            ReactionMeddraPT = reaction.ReactionMeddraPT,
                            TermHighlighted = reaction.TermHighlighted,
                            ReactionStartDateFormat = reaction.ReactionStartDateFormat,
                            ReactionStartDate = reaction.ReactionStartDate,
                            ReactionEndDateFormat = reaction.ReactionEndDateFormat,
                            ReactionEndDate = reaction.ReactionEndDate,
                            ReactionDurationUnit = reaction.ReactionDurationUnit,
                            ReactionOutcome = reaction.ReactionOutcome == 0 ? null : reaction.ReactionOutcome,
                        });
                    }
                }

                // Map drugs if they exist
                if (entity.Patient.Drugs != null && entity.Patient.Drugs.Any())
                {
                    foreach (var drug in entity.Patient.Drugs)
                    {
                        var drugModel = new Drug
                        {
                            Id = drug.Id,
                            DrugCharacterization = drug.DrugCharacterization ?? 0,
                            MedicinalProduct = drug.MedicinalProduct,
                            TradeName = drug.TradeName,
                            //ActiveSubstanceName = drug.ActiveSubstanceName,
                            ObtainDrugCountry = drug.ObtainDrugCountry,
                            DrugAdministrationRoute = drug.DrugAdministrationRoute,
                            DrugAuthorizationCountry = drug.DrugAuthorizationCountry,
                            DrugStartDateFormat = drug.DrugStartDateFormat,
                            DrugStartDate = drug.DrugStartDate,
                            DrugEndDateFormat = drug.DrugEndDateFormat,
                            DrugEndDate = drug.DrugEndDate,
                            DrugIndicationMeddraVersion = drug.DrugIndicationMeddraVersion,
                            DrugIndication = drug.DrugIndication,
                            ActionDrug = drug.ActionDrug,
                            //DrugRecurReadministration = drug.DrugRecurReadministration ?? null,
                            DrugReactionRelatedness = new List<DrugReactionRelatedness>()
                        };

                        // Map active substance if exists
                        if (drug.ActiveSubstance != null)
                        {
                            drugModel.ActiveSubstance = new ActiveSubstance
                            {
                                ActiveSubstanceName = drug.ActiveSubstance.ActiveSubstanceName
                            };
                        }

                        // Map drug reaction relatedness if exists
                        if (drug.DrugReactionRelatedness != null && drug.DrugReactionRelatedness.Any())
                        {
                            foreach (var relatedness in drug.DrugReactionRelatedness)
                            {
                                drugModel.DrugReactionRelatedness.Add(new DrugReactionRelatedness
                                {
                                    DrugReactionAssesMeddraVersion = relatedness.DrugReactionAssesMeddraVersion,
                                    DrugReactionAsses = relatedness.DrugReactionAsses,
                                    DrugAssessmentSource = relatedness.DrugAssessmentSource,
                                    DrugAssessmentMethod = relatedness.DrugAssessmentMethod,
                                    DrugResult = relatedness.DrugResult
                                });
                            }
                        }

                        model.SafetyReport.Patient.Drugs.Add(drugModel);
                    }
                }

                // Map summary if exists
                if (entity.Patient.Summary != null)
                {
                    model.SafetyReport.Patient.Summary = new Summary
                    {
                        NarrativeIncludeClinical = entity.Patient.Summary.NarrativeIncludeClinical
                    };
                }
            }

            // Map concomitant medications if they exist
            if (entity.ConcomitantMedications != null && entity.ConcomitantMedications.Any())
            {
                foreach (var medication in entity.ConcomitantMedications)
                {
                    model.ConcomitantMedications.Add(new ConcomitantMedication
                    {
                        MedicationName = medication.MedicationName,
                        ActiveIngredient = medication.ActiveIngredient,
                        MedicationCountry = medication.MedicationCountry,
                        StartDateFormat = medication.StartDateFormat,
                        StartDate = medication.StartDate,
                        EndDateFormat = medication.EndDateFormat,
                        EndDate = medication.EndDate
                    });
                }
            }

            // Map concomitant diseases if they exist
            if (entity.ConcomitantDiseases != null && entity.ConcomitantDiseases.Any())
            {
                foreach (var disease in entity.ConcomitantDiseases)
                {
                    model.ConcomitantDiseases.Add(new ConcomitantDisease
                    {
                        DiseaseName = disease.DiseaseName,
                        DiseaseType = disease.DiseaseType,
                        StartDateFormat = disease.StartDateFormat,
                        StartDate = disease.StartDate,
                        Continuing = disease.Continuing,
                        EndDateFormat = disease.EndDateFormat,
                        EndDate = disease.EndDate
                    });
                }
            }

            return model;
        }

        private IchIcsrXMLModel CleanNullValues(IchIcsrXMLModel model)
        {
            // Create a deep copy if needed
            // IchIcsrXMLModel cleanModel = DeepCopy(model);

            // Or work with the existing model directly

            // Clean MessageHeader properties
            if (model.MessageHeader != null)
            {
                if (string.IsNullOrEmpty(model.MessageHeader.MessageType)) model.MessageHeader.MessageType = null;
                if (string.IsNullOrEmpty(model.MessageHeader.MessageFormatVersion)) model.MessageHeader.MessageFormatVersion = null;
                if (string.IsNullOrEmpty(model.MessageHeader.MessageFormatRelease)) model.MessageHeader.MessageFormatRelease = null;
                // Add other MessageHeader properties
            }

            // Clean SafetyReport properties
            if (model.SafetyReport != null)
            {
                if (string.IsNullOrEmpty(model.SafetyReport.SafetyReportId)) model.SafetyReport.SafetyReportId = null;
                if (string.IsNullOrEmpty(model.SafetyReport.PrimarySourceCountry)) model.SafetyReport.PrimarySourceCountry = null;
                if (string.IsNullOrEmpty(model.SafetyReport.OccurCountry)) model.SafetyReport.OccurCountry = null;
                if (string.IsNullOrEmpty(model.SafetyReport.TransmissionDate)) model.SafetyReport.TransmissionDate = null;
                if (string.IsNullOrEmpty(model.SafetyReport.ReceiveDate)) model.SafetyReport.ReceiveDate = null;
                if (string.IsNullOrEmpty(model.SafetyReport.ReceiptDate)) model.SafetyReport.ReceiptDate = null;
                if (string.IsNullOrEmpty(model.SafetyReport.CompanyNumber)) model.SafetyReport.CompanyNumber = null;
                if (string.IsNullOrEmpty(model.SafetyReport.CompanyManufacturer)) model.SafetyReport.CompanyManufacturer = null;

                // Add other numeric properties

                // Clean PrimarySource if exists
                if (model.SafetyReport.PrimarySource != null)
                {
                    if (string.IsNullOrEmpty(model.SafetyReport.PrimarySource.ReporterFamilyName))
                        model.SafetyReport.PrimarySource.ReporterFamilyName = null;
                    // Clean other PrimarySource properties

                    // Remove PrimarySource if all properties are null
                    if (IsAllPropertiesNull(model.SafetyReport.PrimarySource))
                        model.SafetyReport.PrimarySource = null;
                }

                // Clean Patient if exists
                if (model.SafetyReport.Patient != null)
                {
                    if (string.IsNullOrEmpty(model.SafetyReport.Patient.PatientInitial))
                        model.SafetyReport.Patient.PatientInitial = null;
                    // Clean other Patient properties

                    // Clean Reactions collection
                    if (model.SafetyReport.Patient.Reactions != null && model.SafetyReport.Patient.Reactions.Any())
                    {
                        List<Reaction> cleanReactions = new List<Reaction>();
                        foreach (var reaction in model.SafetyReport.Patient.Reactions)
                        {
                            // Clean reaction properties
                            if (string.IsNullOrEmpty(reaction.PrimarySourceReaction)) reaction.PrimarySourceReaction = null;
                            // Clean other reaction properties

                            // Only add non-empty reactions
                            if (!IsAllPropertiesNull(reaction))
                                cleanReactions.Add(reaction);
                        }

                        if (cleanReactions.Count > 0)
                            model.SafetyReport.Patient.Reactions = cleanReactions;
                        else
                            model.SafetyReport.Patient.Reactions = null;
                    }

                    // Clean Drugs collection (similar to Reactions)
                    if (model.SafetyReport.Patient.Drugs != null && model.SafetyReport.Patient.Drugs.Any())
                    {
                        List<Drug> cleandrugs = new List<Drug>();
                        foreach (var drug in model.SafetyReport.Patient.Drugs)
                        {
                            //// Clean Drugs properties
                            //if (drug.DrugRecurReadministration == 0|| drug.DrugRecurReadministration == null)
                            //{ 
                            //    drug.DrugRecurReadministration = null; 
                            //}

                            // Clean other reaction properties

                            // Only add non-empty reactions
                            if (!IsAllPropertiesNull(drug))
                                cleandrugs.Add(drug);
                        }

                        if (cleandrugs.Count > 0)
                            model.SafetyReport.Patient.Drugs = cleandrugs;
                        else
                            model.SafetyReport.Patient.Drugs = null;
                    }
                    // Remove Patient if all properties and collections are null
                    if (IsAllPropertiesNull(model.SafetyReport.Patient) &&
                        (model.SafetyReport.Patient.Reactions == null || !model.SafetyReport.Patient.Reactions.Any()) &&
                        (model.SafetyReport.Patient.Drugs == null || !model.SafetyReport.Patient.Drugs.Any()) &&
                        model.SafetyReport.Patient.Summary == null)
                    {
                        model.SafetyReport.Patient = null;
                    }
                }
            }

            // Clean ConcomitantMedications collection
            if (model.ConcomitantMedications != null && model.ConcomitantMedications.Any())
            {
                List<ConcomitantMedication> cleanMedications = new List<ConcomitantMedication>();
                foreach (var medication in model.ConcomitantMedications)
                {
                    // Clean medication properties
                    if (string.IsNullOrEmpty(medication.MedicationName)) medication.MedicationName = null;
                    // Clean other properties

                    // Only add non-empty medications
                    if (!IsAllPropertiesNull(medication))
                        cleanMedications.Add(medication);
                }

                if (cleanMedications.Count > 0)
                    model.ConcomitantMedications = cleanMedications;
                else
                    model.ConcomitantMedications = null;
            }

            // Clean ConcomitantDiseases collection (similar to ConcomitantMedications)
            // ...

            return model;
        }
        // Helper method to check if all properties of an object are null or default
        private bool IsAllPropertiesNull<T>(T obj) where T : class
        {
            if (obj == null) return true;

            // Check each property using reflection
            foreach (var prop in typeof(T).GetProperties())
            {
                // Skip collection properties
                if (typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.PropertyType) &&
                    prop.PropertyType != typeof(string))
                    continue;

                var value = prop.GetValue(obj);

                // If any property has a non-null/non-default value, return false
                if (value != null && !IsDefaultValue(value))
                    return false;
            }

            return true;
        }
        // Helper method to check if a value is the default for its type
        private bool IsDefaultValue(object value)
        {
            var type = value.GetType();

            // Handle numeric types
            if (type == typeof(int)) return (int)value == 0;
            if (type == typeof(long)) return (long)value == 0;
            if (type == typeof(decimal)) return (decimal)value == 0;
            if (type == typeof(double)) return (double)value == 0;

            // Handle string
            if (type == typeof(string)) return string.IsNullOrEmpty((string)value);

            // For other types
            return value.Equals(Activator.CreateInstance(type));
        }

        public async Task<bool> CreateIcsrWithExistingDrugsAsync(IchIcsrXMLModelWithDrugIds importedIcsr, List<int> existingDrugIds, int userId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Creating ICSR with existing drugs. SafetyReportId: {SafetyReportId}, DrugCount: {DrugCount}",
                    importedIcsr.SafetyReport.SafetyReportId, existingDrugIds?.Count ?? 0);

                // Validate input
                if (existingDrugIds?.Any() != true)
                {
                    _logger.LogWarning("No existing drug IDs provided");
                    return false;
                }

                // Map to entity (but drugs will be linked separately)
                var mappedEntity = MapToEntity(importedIcsr);
                mappedEntity.CompanyId = companyId;
                mappedEntity.CreatedByUserId = userId;

                // Create ICSR with existing drugs
                var result = await _icsrRepository.CreateAsyncWithExistingDrugs(mappedEntity, existingDrugIds);

                if (result)
                {
                    _logger.LogInformation("Successfully created ICSR with existing drugs. SafetyReportId: {SafetyReportId}",
                        importedIcsr.SafetyReport.SafetyReportId);
                    return result;
                }

                _logger.LogWarning("Failed to create ICSR with existing drugs. SafetyReportId: {SafetyReportId}",
                    importedIcsr.SafetyReport.SafetyReportId);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ICSR with existing drugs. SafetyReportId: {SafetyReportId}",
                    importedIcsr.SafetyReport?.SafetyReportId ?? "unknown");
                throw;
            }
        }
        public async Task<IchIcsrXMLModel> MapToIchIcsrXMLModel(IchIcsrXMLModelWithDrugIds source)
        {
            if (source == null)
                return null;
            List<Drug> drugs = new List<Drug>();
            if (source.SafetyReport.Patient.DrugIds.Any())
            {
                var DrugsEntity = await _dugrRepository.GetByIdsAsync(source.SafetyReport.Patient.DrugIds);
                drugs = MapDrugEntitiesToDrugs(DrugsEntity);
            }
            if (!drugs.Any())
            {
                throw new ArgumentException($"Drugs with IDs  not found in the provided drugs list");
            }
            return new IchIcsrXMLModel
            {
                Id = source.Id,
                Language = source.Language,
                MessageHeader = source.MessageHeader,
                ConcomitantMedications = source.ConcomitantMedications,
                ConcomitantDiseases = source.ConcomitantDiseases,
                SafetyReport = new SafetyReport
                {
                    SafetyReportVersion = source.SafetyReport.SafetyReportVersion,
                    SafetyReportId = source.SafetyReport.SafetyReportId,
                    PrimarySourceCountry = source.SafetyReport.PrimarySourceCountry,
                    OccurCountry = source.SafetyReport.OccurCountry,
                    TransmissionDateFormat = source.SafetyReport.TransmissionDateFormat,
                    TransmissionDate = source.SafetyReport.TransmissionDate,
                    ReportType = source.SafetyReport.ReportType,
                    Serious = source.SafetyReport.Serious,
                    SeriousnessDeath = source.SafetyReport.SeriousnessDeath,
                    SeriousnessLifeThreatening = source.SafetyReport.SeriousnessLifeThreatening,
                    SeriousnessHospitalization = source.SafetyReport.SeriousnessHospitalization,
                    SeriousnessDisabling = source.SafetyReport.SeriousnessDisabling,
                    SeriousnessCongenitalAnomaly = source.SafetyReport.SeriousnessCongenitalAnomaly,
                    SeriousnessOther = source.SafetyReport.SeriousnessOther,
                    ReceiveDateFormat = source.SafetyReport.ReceiveDateFormat,
                    ReceiveDate = source.SafetyReport.ReceiveDate,
                    ReceiptDateFormat = source.SafetyReport.ReceiptDateFormat,
                    ReceiptDate = source.SafetyReport.ReceiptDate,
                    AdditionalDocument = source.SafetyReport.AdditionalDocument,
                    FulfillExpediteCriteria = source.SafetyReport.FulfillExpediteCriteria,
                    CompanyNumber = source.SafetyReport.CompanyNumber,
                    CompanyManufacturer = source.SafetyReport.CompanyManufacturer,
                    SeriousnessofADR = source.SafetyReport.SeriousnessofADR,
                    PrimarySource = source.SafetyReport.PrimarySource,
                    Sender = source.SafetyReport.Sender,
                    Receiver = source.SafetyReport.Receiver,
                    Patient = new Patient
                    {
                        CaseId = source.SafetyReport.Patient.CaseId,
                        PatientInitial = source.SafetyReport.Patient.PatientInitial,
                        PatientGpMedicalRecordNumber = source.SafetyReport.Patient.PatientGpMedicalRecordNumber,
                        PatientSpecialistRecordNumber = source.SafetyReport.Patient.PatientSpecialistRecordNumber,
                        PatientHospitalRecordNumber = source.SafetyReport.Patient.PatientHospitalRecordNumber,
                        PatientInvestigationNumber = source.SafetyReport.Patient.PatientInvestigationNumber,
                        PatientBirthDateFormat = source.SafetyReport.Patient.PatientBirthDateFormat,
                        PatientBirthDate = source.SafetyReport.Patient.PatientBirthDate,
                        PatientOnsetAge = source.SafetyReport.Patient.PatientOnsetAge,
                        PatientOnsetAgeUnit = source.SafetyReport.Patient.PatientOnsetAgeUnit,
                        GestationPeriod = source.SafetyReport.Patient.GestationPeriod,
                        GestationPeriodUnit = source.SafetyReport.Patient.GestationPeriodUnit,
                        PatientAgeGroup = source.SafetyReport.Patient.PatientAgeGroup,
                        PatientWeight = source.SafetyReport.Patient.PatientWeight,
                        PatientHeight = source.SafetyReport.Patient.PatientHeight,
                        PatientSex = source.SafetyReport.Patient.PatientSex,
                        LastMenstrualDateFormat = source.SafetyReport.Patient.LastMenstrualDateFormat,
                        PatientLastMenstrualDate = source.SafetyReport.Patient.PatientLastMenstrualDate,
                        Reactions = source.SafetyReport.Patient.Reactions,
                        Summary = source.SafetyReport.Patient.Summary,
                        // Map drug IDs to actual Drug objects
                        Drugs = drugs ?? new List<Drug>()
                    }
                }
            };
        }
        public List<Drug> MapDrugEntitiesToDrugs(List<DrugEntity> drugEntities)
        {
            return drugEntities.Select(d => new Drug
            {
                DrugCharacterization = d.DrugCharacterization ?? 0,
                MedicinalProduct = d.MedicinalProduct,
                TradeName = d.TradeName,
                ObtainDrugCountry = d.ObtainDrugCountry,
                DrugAdministrationRoute = d.DrugAdministrationRoute,
                DrugAuthorizationCountry = d.DrugAuthorizationCountry,
                DrugStartDateFormat = d.DrugStartDateFormat,
                DrugStartDate = d.DrugStartDate,
                DrugEndDateFormat = d.DrugEndDateFormat,
                DrugEndDate = d.DrugEndDate,
                DrugIndicationMeddraVersion = d.DrugIndicationMeddraVersion,
                DrugIndication = d.DrugIndication,
                ActionDrug = d.ActionDrug,
            }).ToList();
        }

        //
        public async Task<bool> UpdateIcsrWithDrugsAsync(int icsrId, IchIcsrXMLModelWithDrugIds updatedIcsr, List<int> drugIds, int userId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Updating ICSR with ID: {IcsrId}, DrugCount: {DrugCount}",
                    icsrId, drugIds?.Count ?? 0);

                // Validate input
                if (updatedIcsr?.SafetyReport == null)
                {
                    _logger.LogWarning("Invalid ICSR data provided for update");
                    return false;
                }

                // Check if ICSR exists and user has permission to update it
                var existingIcsr = await _icsrRepository.GetByIdAsync(icsrId, companyId);
                if (existingIcsr == null)
                {
                    _logger.LogWarning("ICSR with ID {IcsrId} not found", icsrId);
                    return false;
                }

                // Check company access if applicable
                if (companyId.HasValue && existingIcsr.CompanyId != companyId)
                {
                    _logger.LogWarning("User does not have permission to update ICSR {IcsrId}", icsrId);
                    return false;
                }

                // Map updated data to entity
                var mappedEntity = MapToEntity(updatedIcsr);
                mappedEntity.Id = icsrId; // Preserve the existing ID
                mappedEntity.CompanyId = existingIcsr.CompanyId; // Preserve company
                mappedEntity.CreatedByUserId = existingIcsr.CreatedByUserId; // Preserve creator
                mappedEntity.ModifiedByUserId = userId; // Set modifier
                mappedEntity.ModifiedDate = DateTime.UtcNow;

                // Update ICSR with new drug associations
                var result = await _icsrRepository.UpdateWithDrugsAsync(mappedEntity, drugIds ?? new List<int>());

                if (result)
                {
                    _logger.LogInformation("Successfully updated ICSR with ID: {IcsrId}", icsrId);
                }
                else
                {
                    _logger.LogWarning("Failed to update ICSR with ID: {IcsrId}", icsrId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating ICSR with ID: {IcsrId}", icsrId);
                throw;
            }
        }

        ///////////
        public async Task<IchIcsrXMLModel> CreateIcsrAsync(IchIcsrXMLModel importedIcsr, int userId, int? companyId)
        {
            try
            {
                _logger.LogInformation("Creating or updating ICSR with safety report ID: {SafetyReportId}", importedIcsr.SafetyReport.SafetyReportId);

                // Check if the report has drugs
                if (importedIcsr.SafetyReport?.Patient?.Drugs?.Any() == true)
                {
                    // Check for existing drugs in the repository
                    var existingDrugIds = await _dugrRepository.GetExistingDrugIdsAsync(importedIcsr.SafetyReport.Patient.Drugs.Select(d => d.MedicinalProduct.Trim().ToLower()).ToList());
                    if (existingDrugIds?.Any() == true)
                    {
                        _logger.LogInformation("Found {DrugCount} existing drugs for ICSR with safety report ID: {SafetyReportId}",
                            existingDrugIds.Count, importedIcsr.SafetyReport.SafetyReportId);

                        // Convert to IchIcsrXMLModelWithDrugIds
                        var modelWithDrugIds = ConvertToModelWithDrugIds(importedIcsr, existingDrugIds);

                        // Call CreateIcsrWithExistingDrugsAsync
                        var result = await CreateIcsrWithExistingDrugsAsync(modelWithDrugIds, existingDrugIds, userId, companyId);
                        if (result)
                        {
                            _logger.LogInformation("Successfully created ICSR with existing drugs for safety report ID: {SafetyReportId}",
                                importedIcsr.SafetyReport.SafetyReportId);
                            return importedIcsr;
                        }
                        else
                        {
                            _logger.LogWarning("Failed to create ICSR with existing drugs for safety report ID: {SafetyReportId}",
                                importedIcsr.SafetyReport.SafetyReportId);
                            return null;
                        }
                    }
                }

                // If no existing drugs, proceed with original creation logic
                var mappedEntity = MapToEntity(importedIcsr);
                mappedEntity.CompanyId = companyId;
                mappedEntity.CreatedByUserId = userId;
                var createResult = await _icsrRepository.CreateAsync(mappedEntity);

                if (createResult)
                {
                    _logger.LogInformation("Successfully created ICSR with safety report ID: {SafetyReportId}",
                        importedIcsr.SafetyReport.SafetyReportId);
                    return importedIcsr;
                }

                _logger.LogWarning("Failed to create ICSR with safety report ID: {SafetyReportId}",
                    importedIcsr.SafetyReport.SafetyReportId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating ICSR with safety report ID: {SafetyReportId}",
                    importedIcsr.SafetyReport?.SafetyReportId ?? "unknown");
                throw;
            }
        }
        private IchIcsrXMLModelWithDrugIds ConvertToModelWithDrugIds(IchIcsrXMLModel model, List<int> drugIds)
        {
            return new IchIcsrXMLModelWithDrugIds
            {
                Id = model.Id,
                Language = model.Language,
                MessageHeader = model.MessageHeader,
                ConcomitantMedications = model.ConcomitantMedications,
                ConcomitantDiseases = model.ConcomitantDiseases,
                SafetyReport = new SafetyReportWithDrugIds
                {
                    SafetyReportVersion = model.SafetyReport.SafetyReportVersion,
                    SafetyReportId = model.SafetyReport.SafetyReportId,
                    PrimarySourceCountry = model.SafetyReport.PrimarySourceCountry,
                    OccurCountry = model.SafetyReport.OccurCountry,
                    TransmissionDateFormat = model.SafetyReport.TransmissionDateFormat,
                    TransmissionDate = model.SafetyReport.TransmissionDate,
                    ReportType = model.SafetyReport.ReportType,
                    Serious = model.SafetyReport.Serious,
                    SeriousnessDeath = model.SafetyReport.SeriousnessDeath,
                    SeriousnessLifeThreatening = model.SafetyReport.SeriousnessLifeThreatening,
                    SeriousnessHospitalization = model.SafetyReport.SeriousnessHospitalization,
                    SeriousnessDisabling = model.SafetyReport.SeriousnessDisabling,
                    SeriousnessCongenitalAnomaly = model.SafetyReport.SeriousnessCongenitalAnomaly,
                    SeriousnessOther = model.SafetyReport.SeriousnessOther,
                    ReceiveDateFormat = model.SafetyReport.ReceiveDateFormat,
                    ReceiveDate = model.SafetyReport.ReceiveDate,
                    ReceiptDateFormat = model.SafetyReport.ReceiptDateFormat,
                    ReceiptDate = model.SafetyReport.ReceiptDate,
                    AdditionalDocument = model.SafetyReport.AdditionalDocument,
                    FulfillExpediteCriteria = model.SafetyReport.FulfillExpediteCriteria,
                    CompanyNumber = model.SafetyReport.CompanyNumber,
                    SeriousnessofADR = model.SafetyReport.SeriousnessofADR,
                    CompanyManufacturer = model.SafetyReport.CompanyManufacturer,
                    PrimarySource = model.SafetyReport.PrimarySource,
                    Sender = model.SafetyReport.Sender,
                    Receiver = model.SafetyReport.Receiver,
                    Patient = new PatientWithDrugIds
                    {
                        CaseId = model.SafetyReport.Patient.CaseId,
                        PatientInitial = model.SafetyReport.Patient.PatientInitial,
                        PatientGpMedicalRecordNumber = model.SafetyReport.Patient.PatientGpMedicalRecordNumber,
                        PatientSpecialistRecordNumber = model.SafetyReport.Patient.PatientSpecialistRecordNumber,
                        PatientHospitalRecordNumber = model.SafetyReport.Patient.PatientHospitalRecordNumber,
                        PatientInvestigationNumber = model.SafetyReport.Patient.PatientInvestigationNumber,
                        PatientBirthDateFormat = model.SafetyReport.Patient.PatientBirthDateFormat,
                        PatientBirthDate = model.SafetyReport.Patient.PatientBirthDate,
                        PatientOnsetAge = model.SafetyReport.Patient.PatientOnsetAge,
                        PatientOnsetAgeUnit = model.SafetyReport.Patient.PatientOnsetAgeUnit,
                        GestationPeriod = model.SafetyReport.Patient.GestationPeriod,
                        GestationPeriodUnit = model.SafetyReport.Patient.GestationPeriodUnit,
                        PatientAgeGroup = model.SafetyReport.Patient.PatientAgeGroup,
                        PatientWeight = model.SafetyReport.Patient.PatientWeight,
                        PatientHeight = model.SafetyReport.Patient.PatientHeight,
                        PatientSex = model.SafetyReport.Patient.PatientSex,
                        LastMenstrualDateFormat = model.SafetyReport.Patient.LastMenstrualDateFormat,
                        PatientLastMenstrualDate = model.SafetyReport.Patient.PatientLastMenstrualDate,
                        Reactions = model.SafetyReport.Patient.Reactions,
                        DrugIds = drugIds,
                        Summary = model.SafetyReport.Patient.Summary
                    }
                }
            };
        }
    }

}