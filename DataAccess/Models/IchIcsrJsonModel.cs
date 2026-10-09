using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class IchIcsrJsonModel
    {
        [Required]
        public IchIcsrMessageHeaderJson MessageHeader { get; set; }

        [Required]
        public SafetyReportJson SafetyReport { get; set; }

        public List<ConcomitantMedicationJson> ConcomitantMedications { get; set; }

        public List<ConcomitantDiseaseJson> ConcomitantDiseases { get; set; }

        public string Language { get; set; }
    }

    public class IchIcsrMessageHeaderJson
    {
        [Required]
        [RegularExpression("ichicsr|backlog|backlogct|psur|ctasr", ErrorMessage = "Invalid messagetype.")]
        public string MessageType { get; set; }

        [Required]
        [StringLength(10)]
        public string MessageFormatVersion { get; set; }

        [Required]
        [StringLength(10)]
        public string MessageFormatRelease { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageNumber { get; set; }

        [Required]
        [StringLength(60)]
        public string MessageSenderIdentifier { get; set; }

        [Required]
        [StringLength(60)]
        public string MessageReceiverIdentifier { get; set; }

        [Required]
        [Range(102, 610)]
        public int MessageDateFormat { get; set; }

        [Required]
        [StringLength(14)]
        public string MessageDate { get; set; }
    }

    public class SafetyReportJson
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int SafetyReportVersion { get; set; }

        [Required]
        [StringLength(60)]
        public string SafetyReportId { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code.")]
        public string OccurCountry { get; set; }

        [Required]
        [Range(102, 610)]
        public int TransmissionDateFormat { get; set; }

        [Required]
        [StringLength(8)]
        public string TransmissionDate { get; set; }

        [Required]
        [Range(1, 4)]
        public int ReportType { get; set; }

        [Required]
        [Range(1, 2)]
        public int Serious { get; set; }

        [Required]
        [Range(102, 610)]
        public int ReceiveDateFormat { get; set; }

        [Required]
        [Range(102, 610)]
        public int ReceiptDateFormat { get; set; }

        [Required]
        [StringLength(8)]
        public string ReceiptDate { get; set; }

        public int? AdditionalDocument { get; set; }

        public int? FulfillExpediteCriteria { get; set; }

        public string CompanyNumber { get; set; }

        public PrimarySourceJson PrimarySource { get; set; }

        public SenderJson Sender { get; set; }

        public ReceiverJson Receiver { get; set; }

        public PatientJson Patient { get; set; }
    }

    public class PrimarySourceJson
    {
        public string ReporterFamilyName { get; set; }

        public int? Qualification { get; set; }
    }

    public class SenderJson
    {
        public int? SenderType { get; set; }

        public string SenderOrganization { get; set; }

        public string SenderDepartment { get; set; }

        public string SenderTitle { get; set; }

        public string SenderGivenName { get; set; }

        public string SenderStreetAddress { get; set; }

        public string SenderTel { get; set; }

        public string SenderEmailAddress { get; set; }
    }

    public class ReceiverJson
    {
        public int? ReceiverType { get; set; }

        public string ReceiverOrganization { get; set; }
    }

    public class PatientJson
    {
        public string PatientInitial { get; set; }

        public int? PatientBirthDateFormat { get; set; }

        public string PatientBirthDate { get; set; }

        public int? PatientAgeGroup { get; set; }

        public decimal? PatientWeight { get; set; }

        public decimal? PatientHeight { get; set; }

        public int? PatientSex { get; set; }

        public List<ReactionJson> Reactions { get; set; }

        public List<DrugJson> Drugs { get; set; }

        public SummaryJson Summary { get; set; }
    }

    public class ReactionJson
    {
        public string PrimarySourceReaction { get; set; }

        public string ReactionMeddraVersionLLT { get; set; }

        public string ReactionMeddraLLT { get; set; }

        public string ReactionMeddraVersionPT { get; set; }

        public string ReactionMeddraPT { get; set; }

        public int? TermHighlighted { get; set; }

        public int? ReactionOutcome { get; set; }
    }

    public class DrugJson
    {
        public int? DrugCharacterization { get; set; }

        public string MedicinalProduct { get; set; }

        public string ObtainDrugCountry { get; set; }

        public string DrugAdministrationRoute { get; set; }

        public string DrugIndicationMeddraVersion { get; set; }

        public int? ActionDrug { get; set; }

        public ActiveSubstanceJson ActiveSubstance { get; set; }

        public List<DrugReactionRelatednessJson> DrugReactionRelatedness { get; set; }
    }

    public class ActiveSubstanceJson
    {
        public string ActiveSubstanceName { get; set; }
    }

    public class DrugReactionRelatednessJson
    {
        public string DrugReactionAssesMeddraVersion { get; set; }

        public string DrugReactionAsses { get; set; }

        public string DrugAssessmentSource { get; set; }
    }

    public class SummaryJson
    {
        public string NarrativeIncludeClinical { get; set; }
    }

    public class ConcomitantMedicationJson
    {
        [Required]
        [StringLength(100)]
        public string MedicationName { get; set; }

        [Range(1, 2)]
        public int? ActiveIngredient { get; set; }

        [StringLength(2, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]{2}$")]
        public string MedicationCountry { get; set; }

        [Range(102, 610)]
        public int? StartDateFormat { get; set; }

        [StringLength(8)]
        public string StartDate { get; set; }

        [Range(102, 610)]
        public int? EndDateFormat { get; set; }

        [StringLength(8)]
        public string EndDate { get; set; }
    }

    public class ConcomitantDiseaseJson
    {
        [Required]
        [StringLength(100)]
        public string DiseaseName { get; set; }

        [Range(1, 10)]
        public int? DiseaseType { get; set; }

        [Range(102, 610)]
        public int? StartDateFormat { get; set; }

        [StringLength(8)]
        public string StartDate { get; set; }

        [Range(1, 3)]
        public int? Continuing { get; set; }

        [Range(102, 610)]
        public int? EndDateFormat { get; set; }

        [StringLength(8)]
        public string EndDate { get; set; }
    }
}