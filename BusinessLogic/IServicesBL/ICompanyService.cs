using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Company;
using DataAccess.Models.Company;
using DataAccess.Models;
namespace BusinessLogic.IServicesBL
{

    public interface ICompanyService
    {
        Task<PaginatedResponse<CompanyDto>> GetPagedCompaniesAsync(int pageNumber, int pageSize);
        Task<List<CompanyDropDownModel>> GetAllAsync();
        Task<CompanyDto?> GetByIdAsync(int id);
        Task<CompanyDto> CreateAsync(CompanyCreateDto dto);
        Task<bool> UpdateAsync(CompanyUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }



}
