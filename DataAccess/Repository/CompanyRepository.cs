using Azure;
using DataAccess.DatabaseContext;
using DataAccess.Entities;
using DataAccess.IRepository;
using DataAccess.Models.Company;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PharmaDbContext _context;

        public CompanyRepository(PharmaDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompanyDropDownModel>> GetAllAsync()
        {
            return await _context.Companies.Where(x=> !x.IsDeleted).Select(x=>new CompanyDropDownModel
            {
                CompanyId = x.CompanyId,
                CompanyName=x.CompanyName,
            }).ToListAsync();
        }

        public async Task<Company?> GetByIdAsync(int id)
        {
            return await _context.Companies.FirstOrDefaultAsync(x=>x.CompanyId==id&&!x.IsDeleted);
        }

        public async Task<Company> AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> UpdateAsync(Company company)
        {
            var existing = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyId == company.CompanyId && !x.IsDeleted);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.CompanyId == id && !x.IsDeleted);
            if (company == null) return false;

            _context.Companies.Remove(company);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Company>> GetPaginatedComaniesAsync(int pageNumber, int pageSize)
        {
            return await _context.Companies.Where(x => !x.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Companies.CountAsync(x=> !x.IsDeleted);
        }

    }

}
