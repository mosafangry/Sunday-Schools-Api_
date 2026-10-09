using System;
using System.Collections.Generic;

namespace DataAccess.Entities.LiteratureReviewEntities
{
    // ======================= Publisher =======================
    public class Publisher
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Journal> Journals { get; set; }
    }

    // ======================= Journal =======================
    public class Journal
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; } // e.g. 'sjhs'
        public long PublisherId { get; set; }

        public Publisher Publisher { get; set; }
        public ICollection<LiteratureReview> LiteratureReviews { get; set; }
    }

    // ======================= Medication =======================
    public class Medication
    {
        public long Id { get; set; }
        public string TradeName { get; set; } // Updated to include Trade Name
        public string GenericName { get; set; } // Renamed from Name to GenericName
        public ICollection<LiteratureReviewMedication> LiteratureReviewMedications { get; set; }
    }

    // ======================= LiteratureReview =======================
    public class LiteratureReview
    {
        public long Id { get; set; }

        public string Title { get; set; }
        public string DOI { get; set; }
        public string Authors { get; set; }

        public string Volume { get; set; }
        public string Issue { get; set; }
        public string Pages { get; set; }

        public DateTime PublishDate { get; set; }

        public string PdfUrl { get; set; }
        public string ArticleUrl { get; set; }

        public bool IsOpenAccess { get; set; }
        public bool IsFavorite { get; set; }

        public long JournalId { get; set; }
        public Journal Journal { get; set; }

        public ICollection<LiteratureReviewMedication> LiteratureReviewMedications { get; set; }
    }

    // ======================= Join Table: LiteratureReviewMedication =======================
    public class LiteratureReviewMedication
    {
        public long LiteratureReviewId { get; set; }
        public LiteratureReview LiteratureReview { get; set; }

        public long MedicationId { get; set; }
        public Medication Medication { get; set; }
    }
}
