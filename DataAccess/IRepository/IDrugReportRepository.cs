using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IDrugReportRepository
    {
        Task<PagedDrugReportDto> GetDrugReportAsync(DrugReportRequestDto request);
    }
}
