using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public interface IDrugReportService
    {
        Task<PagedDrugReportDto> GetDrugReportAsync(DrugReportRequestDto request);
    }
}
