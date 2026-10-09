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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            IRoleRepository roleRepository,
            IMapper mapper,
            ILogger<RoleService> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _roleRepository.GetRoleWithPermissionsAsync(roleId);
                return role != null ? _mapper.Map<RoleDto>(role) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting role by ID: {RoleId}", roleId);
                throw;
            }
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RoleDto>>(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all roles");
                throw;
            }
        }

        public async Task<RoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            try
            {
                var role = _mapper.Map<Role>(createRoleDto);
                await _roleRepository.AddAsync(role);
                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("Role created successfully: {RoleId}", role.RoleId);
                return _mapper.Map<RoleDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating role: {RoleName}", createRoleDto.Name);
                throw;
            }
        }

        public async Task<RoleDto> UpdateRoleAsync(int roleId, UpdateRoleDto updateRoleDto)
        {
            try
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role == null)
                {
                    throw new ArgumentException("Role not found");
                }

                _mapper.Map(updateRoleDto, role);
                await _roleRepository.UpdateAsync(role);
                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("Role updated successfully: {RoleId}", roleId);
                return _mapper.Map<RoleDto>(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating role: {RoleId}", roleId);
                throw;
            }
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            try
            {
                await _roleRepository.DeleteAsync(roleId);
                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("Role deleted successfully: {RoleId}", roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting role: {RoleId}", roleId);
                throw;
            }
        }

        public async Task<IEnumerable<PermissionDto>> GetRolePermissionsAsync(int roleId)
        {
            try
            {
                var permissions = await _roleRepository.GetRolePermissionsAsync(roleId);
                return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting role permissions: {RoleId}", roleId);
                throw;
            }
        }

        public async Task AssignPermissionToRoleAsync(int roleId, int permissionId)
        {
            try
            {
                await _roleRepository.AssignPermissionToRoleAsync(roleId, permissionId);
                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("Permission {PermissionId} assigned to role {RoleId}", permissionId, roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning permission to role: {RoleId}, {PermissionId}", roleId, permissionId);
                throw;
            }
        }

        public async Task RemovePermissionFromRoleAsync(int roleId, int permissionId)
        {
            try
            {
                await _roleRepository.RemovePermissionFromRoleAsync(roleId, permissionId);
                await _roleRepository.SaveChangesAsync();

                _logger.LogInformation("Permission {PermissionId} removed from role {RoleId}", permissionId, roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing permission from role: {RoleId}, {PermissionId}", roleId, permissionId);
                throw;
            }
        }
    }

}
