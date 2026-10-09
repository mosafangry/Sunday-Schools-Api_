using DataAccess.Entities;
using DataAccess.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface ICompanyRepository
    {
        Task<List<CompanyDropDownModel>> GetAllAsync();
        Task<List<Company>> GetPaginatedComaniesAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Company?> GetByIdAsync(int id);
        Task<Company> AddAsync(Company company);
        Task<bool> UpdateAsync(Company company);
        Task<bool> DeleteAsync(int id);
    }

}
