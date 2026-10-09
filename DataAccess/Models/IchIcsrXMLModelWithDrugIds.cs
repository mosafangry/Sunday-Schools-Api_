using DataAccess.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace DataAccess.Models
{
    [XmlRoot("ichicsr")]
    public class IchIcsrXMLModelWithDrugIds
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
        public SafetyReportWithDrugIds SafetyReport { get; set; }

        [XmlArray("ConcomitantMedications")]
        [XmlArrayItem("ConcomitantMedication")]
        public List<ConcomitantMedication> ConcomitantMedications { get; set; }

        [XmlArray("ConcomitantDiseases")]
        [XmlArrayItem("ConcomitantDisease")]
        public List<ConcomitantDisease>? ConcomitantDiseases { get; set; }
    }

    public class SafetyReportWithDrugIds
    {
        [XmlElement("safetyreportversion")]
        [Required]
        [Range(1, int.MaxValue)]
        public int SafetyReportVersion { get; set; }

        [XmlElement("safetyreportid")]
        [Required(ErrorMessage = "Safety report ID is mandatory")]
        [StringLength(100, ErrorMessage = "Safety report ID cannot exceed 100 characters")]
        public string SafetyReportId { get; set; }

        [XmlElement("primarysourcecountry")]
        [StringLength(2, ErrorMessage = "Country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? PrimarySourceCountry { get; set; }

        [XmlElement("occurcountry")]
        [StringLength(2, ErrorMessage = "Country code must be 2 characters")]
        [RegularExpression(@"^[A-Z]{2}$", ErrorMessage = "Must be a valid ISO3166 country code")]
        public string? OccurCountry { get; set; }

        [XmlElement("transmissiondateformat")]
        [AllowedValues(102, ErrorMessage = "Invalid transmission date format Allowed Values: 102 only.")]
        public int? TransmissionDateFormat { get; set; }

        [XmlElement("transmissiondate")]
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
        public PatientWithDrugIds Patient { get; set; }
    }

    public class PatientWithDrugIds
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
        public string? PatientLastMenstrualDate { get; set; }

        [XmlElement("reaction")]
        [Required(ErrorMessage = "At least one reaction is required")]
        public List<Reaction> Reactions { get; set; }

        // CHANGED: List of Drug objects replaced with List of drug IDs
        [XmlArray("drugids")]
        [XmlArrayItem("drugid")]
        [Required(ErrorMessage = "At least one drug ID is required")]
        public List<int> DrugIds { get; set; }

        [XmlElement("summary")]
        [Required(ErrorMessage = "Summary is mandatory")]
        public Summary Summary { get; set; }
    }
}