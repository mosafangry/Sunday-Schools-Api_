using DataAccess.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataAccess.Models
{
    [XmlRoot("ichicsr")]

    public class IchIcsrXMLModel
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlAttribute("lang")]
        [Required(ErrorMessage = "Language attribute is mandatory")]
        [StringLength(10, ErrorMessage = "Language must be a valid ISO639 code")]
        public string Language { get; set; }

        [XmlElement("ichicsrmessageheader")]
        [Required(ErrorMessage = "Message header is mandatory")]
        public IchIcsrMessageHeader MessageHeader { get; set; }

        [XmlElement("safetyreport")]
        [Required(ErrorMessage = "Safety report is mandatory")]
        public SafetyReport SafetyReport { get; set; }

        [XmlArray("ConcomitantMedications")]
        [XmlArrayItem("ConcomitantMedication")]
        public List<ConcomitantMedication> ConcomitantMedications { get; set; }

        [XmlArray("ConcomitantDiseases")]
        [XmlArrayItem("ConcomitantDisease")]
        public List<ConcomitantDisease>? ConcomitantDiseases { get; set; }
    }

    public class IchIcsrMessageHeader
    {
        [XmlElement("messagetype")]
        [Required(ErrorMessage = "Message type is mandatory")]
        [RegularExpression("^(ichicsr|backlog|backlogct|psur|ctasr)$", ErrorMessage = "Invalid message type")]
        [StringLength(16, ErrorMessage = "Message type cannot exceed 16 characters")]
        public string MessageType { get; set; }

        [XmlElement("messageformatversion")]
        [Required(ErrorMessage = "Message format version is mandatory")]
        [StringLength(3, ErrorMessage = "Message format version cannot exceed 3 characters")]
        [RegularExpression("^2\\.1$", ErrorMessage = "Message format version must be 2.1")]
        public string MessageFormatVersion { get; set; }

        [XmlElement("messageformatrelease")]
        [Required(ErrorMessage = "Message format release is mandatory")]
        [StringLength(3, ErrorMessage = "Message format release cannot exceed 3 characters")]
        [RegularExpression("^(1|1\\.0|2|2\\.0)$", ErrorMessage = "Invalid message format release")]
        public string MessageFormatRelease { get; set; }

        [XmlElement("messagenumb")]
        [Required(ErrorMessage = "Message number is mandatory")]
        [StringLength(100, ErrorMessage = "Message number cannot exceed 100 characters")]
        public string MessageNumber { get; set; }

        [XmlElement("messagesenderidentifier")]
        [Required(ErrorMessage = "Message sender identifier is mandatory")]
        [StringLength(60, ErrorMessage = "Message sender identifier cannot exceed 60 characters")]
        public string MessageSenderIdentifier { get; set; }

        [XmlElement("messagereceiveridentifier")]
        [Required(ErrorMessage = "Message receiver identifier is mandatory")]
        [StringLength(60, ErrorMessage = "Message receiver identifier cannot exceed 60 characters")]
        public string MessageReceiverIdentifier { get; set; }

        [XmlElement("messagedateformat")]
        [Required(ErrorMessage = "Message date format is mandatory")]
        [Range(204, 204, ErrorMessage = "Message date format must be 204")]
        public int MessageDateFormat { get; set; }

        [XmlElement("messagedate")]
        [Required(ErrorMessage = "Message date is mandatory")]
        [StringLength(14, ErrorMessage = "Message date must be 14 characters")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "Message date must be a 14-digit number")]
        public string MessageDate { get; set; }
    }

    public class SafetyReport
    {
        [XmlElement("safetyreportversion")]
        [Required]
        [Range(1, int.MaxValue)]
        public int SafetyReportVersion { get; set; }
        [XmlElement("safetyreportid")]
        [Required(ErrorMessage = "Safety report ID is mandatory")]
        [StringLength(100, ErrorMessage = "Safety report ID cannot exceed 100 characters")]
        //[RegularExpression(@"^[A-Z]{2}-[\w\-]+$", ErrorMessage = "Safety report ID must be a valid ISO3166 country code followed by a unique identifier")]
        public string SafetyReportId { get; set; }

        [XmlElement("primarysourcecountry")]
        // [Required(ErrorMessage = "Primary source country is mandatory")]
        [StringLength(2, ErrorMessage = "Country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? PrimarySourceCountry { get; set; }

        [XmlElement("occurcountry")]
        [StringLength(2, ErrorMessage = "Country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? OccurCountry { get; set; }

        [XmlElement("transmissiondateformat")]
        //  [Required(ErrorMessage = "Transmission date format is mandatory")]
        [AllowedValues(102, ErrorMessage = "Invalid transmission date format Allowed Values: 102 only.")]
        public int? TransmissionDateFormat { get; set; }

        [XmlElement("transmissiondate")]
        // [Required(ErrorMessage = "Transmission date is mandatory")]
        [StringLength(8, ErrorMessage = "Transmission date must be 8 characters")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Transmission date must be an 8-digit number")]
        public string? TransmissionDate { get; set; }

        [XmlElement("reporttype")]
        [Required(ErrorMessage = "Report type is mandatory")]
        [Range(1, 4, ErrorMessage = "Report type must be between 1 and 4")]
        public int ReportType { get; set; }

        [XmlElement("serious")]
        [Required(ErrorMessage = "Serious flag is mandatory")]
        [Range(1, 2, ErrorMessage = "Serious flag must be 1 (Yes) or 2 (No)")]
        public int Serious { get; set; }

        [XmlElement("seriousnessdeath")]
        [Range(1, 2, ErrorMessage = "Seriousness death flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessDeath { get; set; }

        [XmlElement("seriousnesslifethreatening")]
        [Range(1, 2, ErrorMessage = "Seriousness life-threatening flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessLifeThreatening { get; set; }

        [XmlElement("seriousnesshospitalization")]
        [Range(1, 2, ErrorMessage = "Seriousness hospitalization flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessHospitalization { get; set; }

        [XmlElement("seriousnessdisabling")]
        [Range(1, 2, ErrorMessage = "Seriousness disabling flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessDisabling { get; set; }

        [XmlElement("seriousnesscongenitalanomali")]
        [Range(1, 2, ErrorMessage = "Seriousness congenital anomaly flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessCongenitalAnomaly { get; set; }

        [XmlElement("seriousnessother")]
        [Range(1, 2, ErrorMessage = "Seriousness other flag must be 1 (Yes) or 2 (No)")]
        public int? SeriousnessOther { get; set; }

        [XmlElement("receivedateformat")]
        [Required(ErrorMessage = "Receive date format is mandatory")]
        [Range(102, 610, ErrorMessage = "Invalid receive date format")]
        public int ReceiveDateFormat { get; set; }

        [XmlElement("receivedate")]
        [Required(ErrorMessage = "Receive date is mandatory")]
        [StringLength(8, ErrorMessage = "Receive date must be 8 characters")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Receive date must be an 8-digit number")]
        public string ReceiveDate { get; set; }

        [XmlElement("receiptdateformat")]
        [Required(ErrorMessage = "Receipt date format is mandatory")]
        [AllowedValues(102, ErrorMessage = "Invalid receipt date format Allowed Values: 102 only.")]
        public int ReceiptDateFormat { get; set; }

        [XmlElement("receiptdate")]
        [Required(ErrorMessage = "Receipt date is mandatory")]
        [StringLength(8, ErrorMessage = "Receipt date must be 8 characters")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Receipt date must be an 8-digit number")]
        public string ReceiptDate { get; set; }

        [XmlElement("additionaldocument")]
        [Range(1, 2, ErrorMessage = "Additional document flag must be 1 (Yes) or 2 (No)")]
        public int? AdditionalDocument { get; set; }

        [XmlElement("fulfillexpeditecriteria")]
        [Range(1, 2, ErrorMessage = "Expedite criteria flag must be 1 (Yes) or 2 (No)")]
        public int? FulfillExpediteCriteria { get; set; }

        [XmlElement("companynumb")]
        [StringLength(100, ErrorMessage = "Company number cannot exceed 100 characters")]
        // [RegularExpression(@"^[A-Z]{2}-[\w\-]+$", ErrorMessage = "Company number must be a valid ISO3166 country code followed by a unique identifier")]
        public string? CompanyNumber { get; set; }
        [XmlIgnore]
        public string? CompanyManufacturer { get; set; }
        [XmlIgnore]
        public int? SeriousnessofADR { get; set; }
        [XmlElement("primarysource")]
        public PrimarySource PrimarySource { get; set; }

        [XmlElement("sender")]
        [Required(ErrorMessage = "Sender information is mandatory")]
        public Sender Sender { get; set; }

        [XmlElement("receiver")]
        [Required(ErrorMessage = "Receiver information is mandatory")]
        public Receiver Receiver { get; set; }

        [XmlElement("patient")]
        [Required(ErrorMessage = "Patient information is mandatory")]
        public Patient Patient { get; set; }
    }

    public class PrimarySource
    {
        [XmlElement("reporterfamilyname")]
        [StringLength(50, ErrorMessage = "Reporter family name cannot exceed 50 characters")]
        public string ReporterFamilyName { get; set; }

        [XmlElement("reporterorganization")]
        [StringLength(60, ErrorMessage = "Reporter organization cannot exceed 60 characters")]
        public string? ReporterOrganization { get; set; }

        [XmlElement("reporterstate")]
        [StringLength(40, ErrorMessage = "Reporter state cannot exceed 40 characters")]
        public string? ReporterState { get; set; }

        [XmlElement("reporterpostcode")]
        [StringLength(15, ErrorMessage = "Reporter postcode cannot exceed 15 characters")]
        public string? ReporterPostCode { get; set; }

        [XmlElement("reportercountry")]
        [StringLength(2, ErrorMessage = "Reporter country must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? ReporterCountry { get; set; }

        [XmlElement("qualification")]
        [Range(1, 5, ErrorMessage = "Reporter qualification must be between 1 and 5")]
        public int? Qualification { get; set; }
    }

    public class Sender
    {
        [XmlElement("sendertype")]
        [Range(1, 6, ErrorMessage = "Sender type must be between 1 and 6")]
        public int? SenderType { get; set; }

        [XmlElement("senderorganization")]
        [Required(ErrorMessage = "Sender organization is mandatory")]
        [StringLength(60, ErrorMessage = "Sender organization cannot exceed 60 characters")]
        public string SenderOrganization { get; set; }

        [XmlElement("sendercountrycode")]
        [StringLength(2, ErrorMessage = "Sender country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? SenderCountryCode { get; set; }
        [XmlElement("senderdepartment")]
        public string? SenderDepartment { get; set; }

        [XmlElement("sendertitle")]
        //[StringLength(10, ErrorMessage = "Sender country code must be 2 characters")]
        public string? SenderTitle { get; set; }

        [XmlElement("sendergivename")]
        public string SenderGiveName { get; set; }

        [XmlElement("senderstreetaddress")]
        public string SenderStreetAddress { get; set; }

        [XmlElement("sendertel")]
        [StringLength(10, ErrorMessage = "sender tel maxmum length is 10 characters")]
        public string? SenderTel { get; set; }

        [XmlElement("senderemailaddress")]
        public string SenderEmailAddress { get; set; }
    }

    public class Receiver
    {
        [XmlElement("receivertype")]
        [Range(1, 6, ErrorMessage = "Receiver type must be between 1 and 6")]
        public int? ReceiverType { get; set; }

        [XmlElement("receiverorganization")]
        [Required(ErrorMessage = "Receiver organization is mandatory")]
        [StringLength(60, ErrorMessage = "Receiver organization cannot exceed 60 characters")]
        public string ReceiverOrganization { get; set; }

        [XmlElement("receivercountrycode")]
        [StringLength(2, ErrorMessage = "Receiver country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? ReceiverCountryCode { get; set; }
    }

    public class Patient
    {
        [XmlIgnore]
        public string? CaseId { get; set; }

        [XmlElement("patientinitial")]
        [StringLength(10, ErrorMessage = "Patient initial cannot exceed 10 characters")]
        public string PatientInitial { get; set; }

        [XmlElement("patientgpmedicalrecordnumb")]
        [StringLength(20, ErrorMessage = "Patient GP medical record number cannot exceed 20 characters")]
        public string? PatientGpMedicalRecordNumber { get; set; }

        [XmlElement("patientspecialistrecordnumb")]
        [StringLength(20, ErrorMessage = "Patient specialist record number cannot exceed 20 characters")]
        public string? PatientSpecialistRecordNumber { get; set; }

        [XmlElement("patienthospitalrecordnumb")]
        [StringLength(20, ErrorMessage = "Patient hospital record number cannot exceed 20 characters")]
        public string? PatientHospitalRecordNumber { get; set; }

        [XmlElement("patientinvestigationnumb")]
        [StringLength(20, ErrorMessage = "Patient investigation number cannot exceed 20 characters")]
        public string? PatientInvestigationNumber { get; set; }

        [XmlElement("patientbirthdateformat")]
        [AllowedValues(102, ErrorMessage = "Invalid patientbirth date format Allowed Values: 102 only.")]
        public int? PatientBirthDateFormat { get; set; }

        [XmlElement("patientbirthdate")]
        [StringLength(8, ErrorMessage = "Patient birth date must be 8 characters")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Patient birth date must be an 8-digit number")]
        public string? PatientBirthDate { get; set; }

        [XmlElement("patientonsetage")]
        [Range(0, 150, ErrorMessage = "Patient onset age must be between 0 and 150")]
        public decimal? PatientOnsetAge { get; set; }

        [XmlElement("patientonsetageunit")]
        [Range(800, 805, ErrorMessage = "Patient onset age unit must be between 800 and 805")]
        public int? PatientOnsetAgeUnit { get; set; }

        [XmlElement("gestationperiod")]
        [Range(0, 999, ErrorMessage = "Gestation period must be between 0 and 999")]
        public int? GestationPeriod { get; set; }

        [XmlElement("gestationperiodunit")]
        [Range(802, 804, ErrorMessage = "Gestation period unit must be 802, 803, or 804")]
        public int? GestationPeriodUnit { get; set; }

        [XmlElement("patientagegroup")]
        [Range(1, 6, ErrorMessage = "Patient age group must be between 1 and 6")]
        public int? PatientAgeGroup { get; set; }

        [XmlElement("patientweight")]
        [Range(0, 650, ErrorMessage = "Patient weight must be between 0 and 650 kg")]
        public int? PatientWeight { get; set; }

        [XmlElement("patientheight")]
        [Range(0, 250, ErrorMessage = "Patient height must be between 0 and 250 cm")]
        public int? PatientHeight { get; set; }

        [XmlElement("patientsex")]
        [Range(1, 2, ErrorMessage = "Patient sex must be 1 (Male) or 2 (Female)")]
        public int? PatientSex { get; set; }

        [XmlElement("lastmenstrualdateformat")]
        [Range(102, 610, ErrorMessage = "Last menstrual date format must be between 102 and 610")]
        public int? LastMenstrualDateFormat { get; set; }

        [XmlElement("patientlastmenstrualdate")]
        [StringLength(8, ErrorMessage = "Last menstrual date must be 8 characters")]
        //[RegularExpression(@"^\d{8}$", ErrorMessage = "Last menstrual date must be an 8-digit number")]
        public string? PatientLastMenstrualDate { get; set; }
        [XmlElement("reaction")]
        [Required(ErrorMessage = "At least one reaction is required")]
        public List<Reaction> Reactions { get; set; }

        [XmlElement("drug")]
        [Required(ErrorMessage = "At least one drug is required")]
        public List<Drug> Drugs { get; set; }

        [XmlElement("summary")]
        [Required(ErrorMessage = "Summary is mandatory")]
        public Summary Summary { get; set; }
    }

    public class Reaction
    {
        [XmlElement("primarysourcereaction")]
        [Required(ErrorMessage = "PrimarySourceReaction is mandatory")]
        public string PrimarySourceReaction { get; set; }

        [XmlElement("reactionmeddraversionllt")]
        //   [Required(ErrorMessage = "Reaction MedDRA version LLT is mandatory")]
        [StringLength(8, ErrorMessage = "Reaction MedDRA version LLT cannot exceed 8 characters")]
        //   [RegularExpression(@"^\d+\.\d+$", ErrorMessage = "Reaction MedDRA version must be in x.x format")]
        public string? ReactionMeddraVersionLLT { get; set; }

        [XmlElement("reactionmeddrallt")]
        // [Required(ErrorMessage = "Reaction MedDRA LLT is mandatory")]
        [StringLength(250, ErrorMessage = "Reaction MedDRA LLT cannot exceed 250 characters")]
        public string? ReactionMeddraLLT { get; set; }

        [XmlElement("reactionmeddraversionpt")]
        [StringLength(8, ErrorMessage = "Reaction MedDRA version PT cannot exceed 8 characters")]
        //  [RegularExpression(@"^\d+\.\d+$", ErrorMessage = "Reaction MedDRA version must be in x.x format")]
        public string? ReactionMeddraVersionPT { get; set; }

        [XmlElement("reactionmeddrapt")]
        public string? ReactionMeddraPT { get; set; }

        [XmlElement("termhighlighted")]
        [Range(1, 4, ErrorMessage = "Term highlighted must be between 1 and 4")]
        public int? TermHighlighted { get; set; }

        [XmlElement("reactionstartdateformat")]
        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Reaction start date format possible  values are  102, 203, 610, 602")]
        public int? ReactionStartDateFormat { get; set; }

        [XmlElement("reactionstartdate")]
        // [StringLength(12, ErrorMessage = "Reaction start date must be 12 characters")]
        // [RegularExpression(@"^\d{12}$", ErrorMessage = "Reaction start date must be a 12-digit number")]
        public string? ReactionStartDate { get; set; }

        [XmlElement("reactionenddateformat")]
        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Reaction end date format possible  values are  102, 203, 610, 602")]
        public int? ReactionEndDateFormat { get; set; }

        [XmlElement("reactionenddate")]
        //  [StringLength(12, ErrorMessage = "Reaction end date must be 12 characters")]
        //  [RegularExpression(@"^\d{12}$", ErrorMessage = "Reaction end date must be a 12-digit number")]
        public string? ReactionEndDate { get; set; }

        [XmlElement("reactiondurationunit")]
        [Range(801, 807, ErrorMessage = "Reaction duration unit must be between 801 and 807")]
        public int? ReactionDurationUnit { get; set; }

        // [XmlElement("reactionoutcome", IsNullable = true)]

        //   [Required(ErrorMessage = "Reaction outcome is mandatory")]
        // [Range(1, 6, ErrorMessage = "Reaction outcome must be between 1 and 6")]
        // public int? ReactionOutcome { get; set; }
        [XmlIgnore]
        public int? ReactionOutcome { get; set; }

        [XmlElement("reactionoutcome")]
        public string? ReactionOutcomeRaw
        {
            get => ReactionOutcome?.ToString();
            set
            {
                ReactionOutcome = int.TryParse(value, out var result) ? result : (int?)null;
            }
        }

    }

    public class Drug
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement("drugcharacterization")]
        [Required(ErrorMessage = "Drug characterization is mandatory")]
        [Range(1, 3, ErrorMessage = "Drug characterization must be 1 (Suspect), 2 (Concomitant), or 3 (Interacting)")]
        public int DrugCharacterization { get; set; }

        [XmlElement("medicinalproduct")]
        [StringLength(70, ErrorMessage = "Medicinal product cannot exceed 70 characters")]
        public string MedicinalProduct { get; set; }
        [XmlIgnore]
        public string? TradeName { get; set; }

        // [XmlElement("activesubstancename")]
        //[Required(ErrorMessage = "Active substance name is mandatory for suspect or interacting drugs")]
        // [StringLength(100, ErrorMessage = "Active substance name cannot exceed 100 characters")]
        // public string? ActiveSubstanceName { get; set; }

        [XmlElement("obtaindrugcountry")]
        [StringLength(2, ErrorMessage = "Obtain drug country must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? ObtainDrugCountry { get; set; }

        [XmlElement("drugadministrationroute")]
        [AllowedDrugAdministrationRoute]
        public string? DrugAdministrationRoute { get; set; }

        [XmlElement("drugauthorizationcountry")]
        [StringLength(2, ErrorMessage = "Drug authorization country must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? DrugAuthorizationCountry { get; set; }

        [XmlElement("drugstartdateformat")]
        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Drug Start Date Format possible  values are  102, 203, 610, 602")]
        public int? DrugStartDateFormat { get; set; }

        [XmlElement("drugstartdate")]
        // [StringLength(8, ErrorMessage = "Drug start date must be 8 characters")]
        // [RegularExpression(@"^\d{8}$", ErrorMessage = "Drug start date must be an 8-digit number")]
        public string? DrugStartDate { get; set; }

        [XmlElement("drugenddateformat")]
        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Drug End Date Format possible  values are  102, 203, 610, 602")]
        public int? DrugEndDateFormat { get; set; }

        [XmlElement("drugenddate")]
        //  [StringLength(8, ErrorMessage = "Drug end date must be 8 characters")]
        // [RegularExpression(@"^\d{8}$", ErrorMessage = "Drug end date must be an 8-digit number")]
        public string? DrugEndDate { get; set; }

        [XmlElement("drugindicationmeddraversion")]
        [StringLength(8, ErrorMessage = "Drug indication MedDRA version cannot exceed 8 characters")]
        // [RegularExpression(@"^\d+\.\d+$", ErrorMessage = "Drug indication MedDRA version must be in x.x format")]
        public string? DrugIndicationMeddraVersion { get; set; }

        [XmlElement("drugindication")]
        [StringLength(250, ErrorMessage = "Drug indication cannot exceed 250 characters")]
        public string? DrugIndication { get; set; }

        [XmlElement("actiondrug")]
        [Range(1, 6, ErrorMessage = "Action drug must be between 1 and 6")]
        public int? ActionDrug { get; set; }

        // [XmlElement("drugrecureadministration")]
        //// [XmlElement(IsNullable = false)]
        // [Range(1, 3, ErrorMessage = "Drug recur readministration must be 1 (Yes), 2 (No), or 3 (Unknown)")]
        // public int? DrugRecurReadministration { get; set; }
        //[XmlIgnore]
        //[Range(1, 3, ErrorMessage = "Drug recur readministration must be 1 (Yes), 2 (No), or 3 (Unknown)")]
        //public int? DrugRecurReadministration { get; set; }

        //[XmlElement("drugrecureadministration")]
        //public string? DrugRecurReadministrationProxy
        //{
        //    get => DrugRecurReadministration.HasValue ? DrugRecurReadministration.Value.ToString() : null;
        //    set => DrugRecurReadministration = string.IsNullOrWhiteSpace(value) ? (int?)null : int.Parse(value);
        //}

        // public bool ShouldSerializeDrugRecurReadministrationProxy() => DrugRecurReadministration.HasValue;


        [XmlElement("activesubstance")]
        public ActiveSubstance? ActiveSubstance { get; set; }
        [XmlElement("drugreactionrelatedness")]
        public List<DrugReactionRelatedness>? DrugReactionRelatedness { get; set; }
    }
    public class ActiveSubstance
    {
        [XmlElement("activesubstancename")]
        public string? ActiveSubstanceName { get; set; }
    }

    public class DrugReactionRelatedness
    {
        [XmlElement("drugreactionassesmeddraversion")]
        //[Required(ErrorMessage = "Drug reaction assessment MedDRA version is mandatory")]
        [StringLength(8, ErrorMessage = "Drug reaction assessment MedDRA version cannot exceed 8 characters")]
        // [RegularExpression(@"^\d+\.\d+$", ErrorMessage = "Drug reaction assessment MedDRA version must be in x.x format")]
        public string? DrugReactionAssesMeddraVersion { get; set; }

        [XmlElement("drugreactionasses")]
        [Required(ErrorMessage = "Drug reaction assessment is mandatory")]
        [StringLength(250, ErrorMessage = "Drug reaction assessment cannot exceed 250 characters")]
        public string DrugReactionAsses { get; set; }

        [XmlElement("drugassessmentsource")]
        [StringLength(60, ErrorMessage = "Drug assessment source cannot exceed 60 characters")]
        public string DrugAssessmentSource { get; set; }

        [XmlElement("drugassessmentmethod")]
        [StringLength(35, ErrorMessage = "Drug assessment method cannot exceed 35 characters")]
        public string? DrugAssessmentMethod { get; set; }

        [XmlElement("drugresult")]
        [StringLength(35, ErrorMessage = "Drug result cannot exceed 35 characters")]
        public string? DrugResult { get; set; }
    }

    public class Summary
    {
        [XmlElement("narrativeincludeclinical")]
        [Required(ErrorMessage = "Narrative including clinical information is mandatory")]
        public string NarrativeIncludeClinical { get; set; }
    }

    public class ConcomitantMedication
    {
        [Required(ErrorMessage = "Medication name is mandatory")]
        [StringLength(100, ErrorMessage = "Medication name cannot exceed 100 characters")]
        public string MedicationName { get; set; }

        [Range(1, 2, ErrorMessage = "Active ingredient must be 1 or 2")]
        public int? ActiveIngredient { get; set; }

        [StringLength(2, ErrorMessage = "Medication country must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? MedicationCountry { get; set; }
        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Start date Format possible  values are  102, 203, 610, 602")]
        public int? StartDateFormat { get; set; }

        //[StringLength(8, ErrorMessage = "Start date must be 8 characters")]
        //[RegularExpression(@"^\d{8}$", ErrorMessage = "Start date must be an 8-digit number")]
        public string? StartDate { get; set; }

        [AllowedValues(102, 203, 610, 602, ErrorMessage = "End date Format possible  values are  102, 203, 610, 602")]
        public int? EndDateFormat { get; set; }

        // [StringLength(8, ErrorMessage = "End date must be 8 characters")]
        //[RegularExpression(@"^\d{8}$", ErrorMessage = "End date must be an 8-digit number")]
        public string? EndDate { get; set; }
    }

    public class ConcomitantDisease
    {
        // [Required(ErrorMessage = "Disease name is mandatory")]
        [StringLength(100, ErrorMessage = "Disease name cannot exceed 100 characters")]
        public string DiseaseName { get; set; }

        [Range(1, 10, ErrorMessage = "Disease type must be between 1 and 10")]
        public int? DiseaseType { get; set; }

        [AllowedValues(102, 203, 610, 602, ErrorMessage = "Start date Format possible  values are  102, 203, 610, 602")]
        public int? StartDateFormat { get; set; }

        //[StringLength(8, ErrorMessage = "Start date must be 8 characters")]
        //[RegularExpression(@"^\d{8}$", ErrorMessage = "Start date must be an 8-digit number")]
        public string? StartDate { get; set; }

        [Range(1, 3, ErrorMessage = "Continuing status must be 1, 2, or 3")]
        public int? Continuing { get; set; }

        [AllowedValues(102, 203, 610, 602, ErrorMessage = "End date Format possible  values are  102, 203, 610, 602")]
        public int? EndDateFormat { get; set; }

        //[StringLength(8, ErrorMessage = "End date must be 12 characters")]
        //[RegularExpression(@"^\d{12}$", ErrorMessage = "End date must be an 12-digit number")]
        public string? EndDate { get; set; }
    }
}