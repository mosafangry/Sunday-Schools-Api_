using DataAccess.IRepository;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class DrugReportService : IDrugReportService
    {
        private readonly IDrugReportRepository _drugReportRepository;

        public DrugReportService(IDrugReportRepository drugReportRepository)
        {
            _drugReportRepository = drugReportRepository;
        }

        public async Task<PagedDrugReportDto> GetDrugReportAsync(DrugReportRequestDto request)
        {
            return await _drugReportRepository.GetDrugReportAsync(request);
        }
    }
}
