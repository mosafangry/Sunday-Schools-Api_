using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTOs.LiteratureReviewDtos
{
    public class LiteratureReviewDto
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
        public bool IsOpenAccess { get; set; }
        public bool IsFavorite { get; set; }
        public JournalDto Journal { get; set; }
        public IEnumerable<MedicationDto> Medications { get; set; }
    }

    public class CreateLiteratureReviewDto
    {
        
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
        public IEnumerable<long> MedicationIds { get; set; }
    }

    public class JournalDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public PublisherDto Publisher { get; set; }
    }

    public class CreateJournalDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public long PublisherId { get; set; }
    }

    public class UpdateJournalDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public long PublisherId { get; set; }
    }

    public class PublisherDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CreatePublisherDto
    {
        public string Name { get; set; }
    }

    public class UpdatePublisherDto
    {
        public string Name { get; set; }
    }
    public class MedicationDto
    {
        public long Id { get; set; }
        public string TradeName { get; set; }
        public string GenericName { get; set; }
    }

    public class CreateMedicationDto
    {
        public string TradeName { get; set; }
        public string GenericName { get; set; }
    }

    public class UpdateMedicationDto
    {
        public string TradeName { get; set; }
        public string GenericName { get; set; }
    }
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
