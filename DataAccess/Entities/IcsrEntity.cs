using DataAccess.Entities.AuthEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    // Main ICSR report entity
    public class IcsrEntity : AuditableEntity, ITrackable
    {
        [Key]
        public int Id { get; set; }
        // Properties from IchIcsrXMLModel
        public string? Language { get; set; }
        public string? SafetyReportId { get; set; }
        public int? SafetyReportVersion { get; set; }
        public string? PrimarySourceCountry { get; set; }
        public string? OccurCountry { get; set; }
        public int? TransmissionDateFormat { get; set; }
        public string? TransmissionDate { get; set; }
        public int? ReportType { get; set; }
        public int? Serious { get; set; }
        public int? SeriousnessDeath { get; set; }
        public int? SeriousnessLifeThreatening { get; set; }
        public int? SeriousnessHospitalization { get; set; }
        public int? SeriousnessDisabling { get; set; }
        public int? SeriousnessCongenitalAnomaly { get; set; }
        public int? SeriousnessOther { get; set; }
        public int? ReceiveDateFormat { get; set; }
        public string? ReceiveDate { get; set; }
        public int? ReceiptDateFormat { get; set; }
        public string? ReceiptDate { get; set; }
        public int? AdditionalDocument { get; set; }
        public int? FulfillExpediteCriteria { get; set; }
        public string? CompanyNumber { get; set; }
        public string? CompanyManufacturer { get; set; }
        public int? SeriousnessofADR { get; set; }


        // Navigation properties
        public int? MessageHeaderId { get; set; }
        public MessageHeaderEntity? MessageHeader { get; set; }

        public int? PrimarySourceId { get; set; }
        public PrimarySourceEntity? PrimarySource { get; set; }

        public int? SenderId { get; set; }
        public SenderEntity? Sender { get; set; }

        public int? ReceiverId { get; set; }
        public ReceiverEntity? Receiver { get; set; }

        public int? PatientId { get; set; }
        public PatientEntity? Patient { get; set; }

        public ICollection<ConcomitantMedicationEntity>? ConcomitantMedications { get; set; }
        public ICollection<ConcomitantDiseaseEntity>? ConcomitantDiseases { get; set; }

        // Add Company foreign key for data isolation
        //  [Required]
        public int? CompanyId { get; set; }

        // Add CreatedBy user tracking
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
        // Navigation properties
        public virtual Company? Company { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? ModifiedByUser { get; set; }
    }
    public class MessageHeaderEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? MessageType { get; set; }
        public string? MessageFormatVersion { get; set; }
        public string? MessageFormatRelease { get; set; }
        public string? MessageNumber { get; set; }
        public string? MessageSenderIdentifier { get; set; }
        public string? MessageReceiverIdentifier { get; set; }
        public int? MessageDateFormat { get; set; }
        public string? MessageDate { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }

    public class PrimarySourceEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? ReporterFamilyName { get; set; }
        public string? ReporterOrganization { get; set; }
        public string? ReporterState { get; set; }
        public string? ReporterPostCode { get; set; }
        public string? ReporterCountry { get; set; }
        public int? Qualification { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }

    public class SenderEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public int? SenderType { get; set; }
        public string? SenderOrganization { get; set; }
        public string? SenderCountryCode { get; set; }
        public string? SenderDepartment { get; set; }
        public string? SenderTitle { get; set; }
        public string? SenderGiveName { get; set; }
        public string? SenderStreetAddress { get; set; }
        public string? SenderTel { get; set; }
        public string? SenderEmailAddress { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }

    public class ReceiverEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public int? ReceiverType { get; set; }
        public string? ReceiverOrganization { get; set; }
        public string? ReceiverCountryCode { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }

    public class PatientEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string? CaseId { get; set; }

        public string? PatientInitial { get; set; }
        public string? PatientGpMedicalRecordNumber { get; set; }
        public string? PatientSpecialistRecordNumber { get; set; }
        public string? PatientHospitalRecordNumber { get; set; }
        public string? PatientInvestigationNumber { get; set; }
        public int? PatientBirthDateFormat { get; set; }
        public string? PatientBirthDate { get; set; }
        public decimal? PatientOnsetAge { get; set; }
        public int? PatientOnsetAgeUnit { get; set; }
        public int? GestationPeriod { get; set; }
        public int? GestationPeriodUnit { get; set; }
        public int? PatientAgeGroup { get; set; }
        public int? PatientWeight { get; set; }
        public int? PatientHeight { get; set; }
        public int? PatientSex { get; set; }
        public int? LastMenstrualDateFormat { get; set; }
        public string? PatientLastMenstrualDate { get; set; }

        // Navigation properties
        public IcsrEntity? Icsr { get; set; }
        public ICollection<ReactionEntity>? Reactions { get; set; }
        public ICollection<DrugEntity>? Drugs { get; set; }
        public SummaryEntity? Summary { get; set; }
    }

    public class ReactionEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? PrimarySourceReaction { get; set; }
        public string? ReactionMeddraVersionLLT { get; set; }
        public string? ReactionMeddraLLT { get; set; }
        public string? ReactionMeddraVersionPT { get; set; }
        public string? ReactionMeddraPT { get; set; }
        public int? TermHighlighted { get; set; }
        public int? ReactionStartDateFormat { get; set; }
        public string? ReactionStartDate { get; set; }
        public int? ReactionEndDateFormat { get; set; }
        public string? ReactionEndDate { get; set; }
        public int? ReactionDurationUnit { get; set; }
        public int? ReactionOutcome { get; set; }

        // Foreign key
        public int? PatientId { get; set; }

        // Navigation property
        public PatientEntity? Patient { get; set; }
    }

    public class DrugEntity : AuditableEntity, ITrackable
    {
        [Key]
        public int Id { get; set; }

        public int? DrugCharacterization { get; set; }
        public string? MedicinalProduct { get; set; }
        public string? TradeName { get; set; }
        public string? ActiveSubstanceName { get; set; }
        public string? ObtainDrugCountry { get; set; }
        public string? DrugAdministrationRoute { get; set; }
        public string? DrugAuthorizationCountry { get; set; }
        public int? DrugStartDateFormat { get; set; }
        public string? DrugStartDate { get; set; }
        public int? DrugEndDateFormat { get; set; }
        public string? DrugEndDate { get; set; }
        public string? DrugIndicationMeddraVersion { get; set; }
        public string? DrugIndication { get; set; }
        public int? ActionDrug { get; set; }
        public int? DrugRecurReadministration { get; set; }

        // Foreign key
        //many to many
        public ICollection<PatientEntity> Patients { get; set; } = new List<PatientEntity>();

        public ActiveSubstanceEntity? ActiveSubstance { get; set; }
        public ICollection<DrugReactionRelatednessEntity>? DrugReactionRelatedness { get; set; }

        public virtual Company? Company { get; set; }
        public int? CompanyId { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? ModifiedByUserId { get; set; }
    }

    public class ActiveSubstanceEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? ActiveSubstanceName { get; set; }

        // Foreign key
        public int? DrugId { get; set; }

        // Navigation property
        public DrugEntity? Drug { get; set; }
    }

    public class DrugReactionRelatednessEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? DrugReactionAssesMeddraVersion { get; set; }
        public string? DrugReactionAsses { get; set; }
        public string? DrugAssessmentSource { get; set; }
        public string? DrugAssessmentMethod { get; set; }
        public string? DrugResult { get; set; }

        // Foreign key
        public int? DrugId { get; set; }

        // Navigation property
        public DrugEntity? Drug { get; set; }
    }

    public class SummaryEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? NarrativeIncludeClinical { get; set; }

        // Foreign key
        public int? PatientId { get; set; }

        // Navigation property
        public PatientEntity? Patient { get; set; }
    }

    public class ConcomitantMedicationEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? MedicationName { get; set; }
        public int? ActiveIngredient { get; set; }
        public string? MedicationCountry { get; set; }
        public int? StartDateFormat { get; set; }
        public string? StartDate { get; set; }
        public int? EndDateFormat { get; set; }
        public string? EndDate { get; set; }

        // Foreign key
        public int? IcsrId { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }

    public class ConcomitantDiseaseEntity : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        public string? DiseaseName { get; set; }
        public int? DiseaseType { get; set; }
        public int? StartDateFormat { get; set; }
        public string? StartDate { get; set; }
        public int? Continuing { get; set; }
        public int? EndDateFormat { get; set; }
        public string? EndDate { get; set; }

        // Foreign key
        public int? IcsrId { get; set; }

        // Navigation property
        public IcsrEntity? Icsr { get; set; }
    }


}