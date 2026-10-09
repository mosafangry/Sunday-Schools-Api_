using BusinessLogic.IServicesBL;
using DataAccess.Entities;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Company;
using DataAccess.Repositories;
using DataAccess.Models.Company;
using BusinessLogic.DTOs.Drugs;
using DataAccess.Models;
using DataAccess.Repository;


namespace BusinessLogic.ServicesBL
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepo, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _mapper = mapper;
        }

        public async Task<List<CompanyDropDownModel>> GetAllAsync()
        {
            return  await _companyRepo.GetAllAsync();
           //  _mapper.Map<List<CompanyDto>>(entities);
        }

        public async Task<CompanyDto?> GetByIdAsync(int id)
        {
            var entity = await _companyRepo.GetByIdAsync(id);
            return entity == null ? null : _mapper.Map<CompanyDto>(entity);
        }

        public async Task<CompanyDto> CreateAsync(CompanyCreateDto dto)
        {
            var entity = _mapper.Map<Company>(dto);
            var created = await _companyRepo.AddAsync(entity);
            return _mapper.Map<CompanyDto>(created);
        }

        public async Task<bool> UpdateAsync(CompanyUpdateDto dto)
        {
            var entity = await _companyRepo.GetByIdAsync(dto.CompanyId);
            if (entity == null) return false;

            _mapper.Map(dto, entity);
            return await _companyRepo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _companyRepo.DeleteAsync(id);
        }
        public async Task<PaginatedResponse<CompanyDto>> GetPagedCompaniesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page must be greater than 0", nameof(pageNumber));

            if (pageSize <= 0 || pageSize > 100)
                throw new ArgumentException("Page size must be between 1 and 100", nameof(pageSize));

            var ComaniesEntities = await _companyRepo.GetPaginatedComaniesAsync(pageNumber, pageSize);
            var totalCount = await _companyRepo.GetTotalCountAsync();

            var Comanies = _mapper.Map<IEnumerable<CompanyDto>>(ComaniesEntities);
            return new PaginatedResponse<CompanyDto>
            {
                Items = Comanies.ToList(),
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }

}
