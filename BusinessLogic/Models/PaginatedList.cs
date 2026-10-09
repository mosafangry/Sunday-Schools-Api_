using Microsoft.Extensions.Configuration;
namespace BusinessLogic.Models
{
    public static class PaginationSettings
    {
        public static int DefaultPageSize { get; private set; } = 10;
        public static int DefaultExportReportSize { get; private set; } = 10000;

        public static void SetPageSize(IConfiguration configuration)
        {
            if (int.TryParse(configuration["PageSize"], out int configPageSize))
            {
                DefaultPageSize = configPageSize;
            }
            if (int.TryParse(configuration["ExportReportSize"], out int configExportReportSize))
            {
                DefaultExportReportSize = configExportReportSize;
            }
        }
    }
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get;  set; }
        public int TotalPages { get;  set; }
        public int ItemsCount { get;  set; }
        public int PageSize { get;  set; }

        public PaginatedList(List<T> items, int count, int pageIndex, bool IsExportingReport = false)
            : this(items, count, pageIndex, 0, IsExportingReport)
        {
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize, bool IsExportingReport = false)
        {
            PageIndex = pageIndex;
            if (IsExportingReport && pageSize == 0)
            {
                PageSize = PaginationSettings.DefaultExportReportSize;
            }
            else if (!IsExportingReport && pageSize == 0)
            {
                PageSize = PaginationSettings.DefaultPageSize;

            }
            else
            {
                PageSize = pageSize;
            }
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            ItemsCount = count;
            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public static PaginatedList<T> Create(List<T> items, int count, int pageIndex, bool IsExportingReport = false)
        {
            return new PaginatedList<T>(items, count, pageIndex, IsExportingReport);
        }
    }

}
