using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServicesBL
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(CreateUserDto createUserDto);
        Task<bool> ValidateTokenAsync(string token);
        Task<UserDto> GetCurrentUserAsync(int userId);
        Task LogoutAsync(int userId);
        string GenerateJwtToken(UserDto user);
    }
}
