using AutoMapper;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.IServicesBL;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(
            IPermissionRepository permissionRepository,
            IMapper mapper,
            ILogger<PermissionService> logger)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int permissionId)
        {
            try
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                return permission != null ? _mapper.Map<PermissionDto>(permission) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting permission by ID: {PermissionId}", permissionId);
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            try
            {
                var permissions = await _permissionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all permissions");
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsByCategoryAsync(string category)
        {
            try
            {
                var permissions = await _permissionRepository.GetPermissionsByCategory(category);
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting permissions by category: {Category}", category);
                throw;
            }
        }

        public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto createPermissionDto)
        {
            try
            {
                var permission = _mapper.Map<Permission>(createPermissionDto);
                await _permissionRepository.AddAsync(permission);
                await _permissionRepository.SaveChangesAsync();

                _logger.LogInformation("Permission created successfully: {PermissionId}", permission.PermissionId);
                return _mapper.Map<PermissionDto>(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating permission: {PermissionName}", createPermissionDto.Name);
                throw;
            }
        }

        public async Task<PermissionDto> UpdatePermissionAsync(int permissionId, UpdatePermissionDto updatePermissionDto)
        {
            try
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                if (permission == null)
                {
                    throw new ArgumentException("Permission not found");
                }

                _mapper.Map(updatePermissionDto, permission);
                await _permissionRepository.UpdateAsync(permission);
                await _permissionRepository.SaveChangesAsync();

                _logger.LogInformation("Permission updated successfully: {PermissionId}", permissionId);
                return _mapper.Map<PermissionDto>(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating permission: {PermissionId}", permissionId);
                throw;
            }
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            try
            {
                await _permissionRepository.DeleteAsync(permissionId);
                await _permissionRepository.SaveChangesAsync();

                _logger.LogInformation("Permission deleted successfully: {PermissionId}", permissionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting permission: {PermissionId}", permissionId);
                throw;
            }
        }
    }

}
