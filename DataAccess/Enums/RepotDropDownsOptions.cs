using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Enums for EudraVigilance Human - Processing of safety messages and ICSRs
 * Based on EMA/H/20665/04/Final Rev. 2
 */
namespace DataAccess.Enums
{
    /// <summary>
    /// M.1.1 Message Type
    /// </summary>
    public enum MessageType
    {
        ICHICSR = 1,
        BACKLOG = 2,
        BACKLOGCT = 3,
        PSUR = 4,
        CTASR = 5
    }

    /// <summary>
    /// M.1.7a & related date formats
    /// </summary>
    public enum DateFormat
    {
        /// <summary>Basic format for most dates (YYYYMMDD)</summary>
        YYYYMMDD = 102,
        /// <summary>Format with hyphens (YYYY-MM-DD)</summary>
        YYYY_MM_DD = 203,
        /// <summary>Full date with time (YYYYMMDDHHMM)</summary>
        YYYYMMDDHHMM = 204,
        /// <summary>Year and month only</summary>
        YYYYMM = 610,
        /// <summary>Year only</summary>
        YYYY = 602
    }

    /// <summary>
    /// A.1.4 Report Type
    /// </summary>
    public enum ReportType
    {
        SPONTANEOUS = 1,
        REPORT_FROM_STUDY = 2,
        OTHER = 3,
        NOT_AVAILABLE = 4
    }

    /// <summary>
    /// A.1.5.x Seriousness & related fields
    /// </summary>
    public enum YesNo
    {
        YES = 1,
        NO = 2
    }

    /// <summary>
    /// For fields with Yes/No/Unknown options
    /// </summary>
    public enum YesNoUnknown
    {
        YES = 1,
        NO = 2,
        UNKNOWN = 3
    }

    /// <summary>
    /// A.2.1.4 Qualification
    /// </summary>
    public enum ReporterQualification
    {
        PHYSICIAN = 1,
        PHARMACIST = 2,
        OTHER_HEALTH_PROFESSIONAL = 3,
        LAWYER = 4,
        CONSUMER_OR_NON_HEALTH_PROFESSIONAL = 5
    }

    /// <summary>
    /// A.2.3.3 Observe Study Type
    /// </summary>
    public enum ObserveStudyType
    {
        CLINICAL_TRIALS = 1,
        INDIVIDUAL_PATIENT_USE = 2,
        OTHER_STUDIES = 3
    }

    /// <summary>
    /// A.3.1.1 Sender Type
    /// </summary>
    public enum SenderType
    {
        PHARMACEUTICAL_COMPANY = 1,
        REGULATORY_AUTHORITY = 2,
        HEALTH_PROFESSIONAL = 3,
        REGIONAL_PHARMACOVIGILANCE_CENTER = 4,
        WHO_COLLABORATING_CENTER = 5,
        OTHER = 6
    }

    /// <summary>
    /// A.3.2.1 Receiver Type (note: no type 3 in the document)
    /// </summary>
    public enum ReceiverType
    {
        PHARMACEUTICAL_COMPANY = 1,
        REGULATORY_AUTHORITY = 2,
        REGIONAL_PHARMACOVIGILANCE_CENTER = 4,
        WHO_COLLABORATING_CENTER = 5,
        OTHER = 6
    }

    /// <summary>
    /// B.1.2.2b Patient Onset Age Unit
    /// </summary>
    public enum AgeUnit
    {
        DECADE = 800,
        YEAR = 801,
        MONTH = 802,
        WEEK = 803,
        DAY = 804,
        HOUR = 805
    }

    /// <summary>
    /// B.1.2.2.1b Gestation Period Unit
    /// </summary>
    public enum GestationPeriodUnit
    {
        MONTH = 802,
        WEEK = 803,
        DAY = 804,
        TRIMESTER = 810
    }

    /// <summary>
    /// B.1.2.3 Patient Age Group
    /// </summary>
    public enum PatientAgeGroup
    {
        NEONATE = 1,
        INFANT = 2,
        CHILD = 3,
        ADOLESCENT = 4,
        ADULT = 5,
        ELDERLY = 6
    }

    /// <summary>
    /// B.1.5 Patient Sex
    /// </summary>
    public enum PatientSex
    {
        MALE = 1,
        FEMALE = 2
    }

    /// <summary>
    /// B.2.i.3 Term Highlighted
    /// </summary>
    public enum TermHighlighted
    {
        YES_NOT_SERIOUS = 1,
        NO_NOT_SERIOUS = 2,
        YES_SERIOUS = 3,
        NO_SERIOUS = 4
    }

    /// <summary>
    /// B.2.i.6b & related duration units
    /// </summary>
    public enum DurationUnit
    {
        YEAR = 801,
        MONTH = 802,
        WEEK = 803,
        DAY = 804,
        HOUR = 805,
        MINUTE = 806,
        SECOND = 807
    }

    /// <summary>
    /// B.2.i.8 Reaction Outcome
    /// </summary>
    public enum ReactionOutcome
    {
        RECOVERED_RESOLVED = 1,
        RECOVERING_RESOLVING = 2,
        NOT_RECOVERED_NOT_RESOLVED = 3,
        RECOVERED_WITH_SEQUELAE = 4,
        FATAL = 5,
        UNKNOWN = 6
    }

    /// <summary>
    /// B.4.k.1 Drug Characterization
    /// </summary>
    public enum DrugCharacterization
    {
        SUSPECT = 1,
        CONCOMITANT = 2,
        INTERACTING = 3
    }

    /// <summary>
    /// B.4.k.5.2 Drug Structured Dosage Unit
    /// </summary>
    public enum DrugDosageUnit
    {
        KILOGRAM = 1,
        GRAM = 2,
        MILLIGRAM = 3,
        MICROGRAM = 4,
        NANOGRAM = 5,
        PICOGRAM = 6,
        LITER = 7,
        MILLILITER = 8,
        MICROLITER = 9,
        BECQUEREL = 10,
        GIGABECQUEREL = 11,
        MEGABECQUEREL = 12,
        KILOBECQUEREL = 13,
        CURIE = 14,
        MILLICURIE = 15,
        MICROCURIE = 16,
        NANOCURIE = 17,
        MOLE = 18,
        MILLIMOLE = 19,
        MICROMOLE = 20,
        INTERNATIONAL_UNIT = 21,
        KILOUNIT = 22,
        MILLIUNIT = 23,
        MICROUNIT = 24,
        PERCENTAGE = 25,
        MILLIEQUIVALENT = 26,
        MILLIEQUIVALENT_PER_KILOGRAM = 27,
        MILLIEQUIVALENT_PER_LITER = 28,
        HUNDRED_MILLILITER = 29,
        DROP = 30,
        MILLION_UNIT = 31,
        PIECE = 32
    }

    /// <summary>
    /// B.4.k.5.5 Drug Interval Dosage Definition
    /// </summary>
    public enum DrugIntervalDosageDefinition
    {
        YEAR = 801,
        MONTH = 802,
        WEEK = 803,
        DAY = 804,
        HOUR = 805,
        MINUTE = 806,
        SECOND = 807,
        TRIMESTER = 810,
        CYCLICAL = 811,
        AS_NECESSARY = 812,
        TOTAL = 813
    }

    /// <summary>
    /// B.4.k.16 Action Taken with Drug
    /// </summary>
    public enum ActionDrug
    {
        DRUG_WITHDRAWN = 1,
        DOSE_REDUCED = 2,
        DOSE_INCREASED = 3,
        DOSE_NOT_CHANGED = 4,
        UNKNOWN = 5,
        NOT_APPLICABLE = 6
    }

    /// <summary>
    /// B.4.k.18.2 Drug Assessment Source (from Note 22)
    /// </summary>
    public enum DrugAssessmentSource
    {
        INVESTIGATOR = 1,
        SPONSOR = 2,
        COMPANY = 3
    }

    /// <summary>
    /// B.4.k.18.4 Drug Result (from Note 24)
    /// </summary>
    public enum DrugResult
    {
        RELATED = 1,
        NOT_RELATED = 2
    }
}
