using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.User;
using BusinessLogic.IServicesBL;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ServicesBL
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}", loginDto.Email);

                var user = await _userRepository.GetByEmailAsync(loginDto.Email);
                if (user == null || !user.IsActive)
                {
                    _logger.LogWarning("Login failed - user not found or inactive: {Email}", loginDto.Email);
                    return null;
                }

                // Validate password (implement proper password hashing)
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Login failed - invalid password: {Email}", loginDto.Email);
                    return null;
                }

                // Get user with full details
                var userWithDetails = await _userRepository.GetUserWithRolesAsync(user.UserId);
                userWithDetails.UserPermissions = (await _userRepository.GetUserPermissionsAsync(user.UserId)).ToList();
                userWithDetails.UserSubscription = (await _userRepository.GetUserWithSubscriptionAsync(user.UserId))?.UserSubscription;

                var userDto = _mapper.Map<UserDto>(userWithDetails);
                var token = GenerateJwtToken(userDto);

                // Update last login time
                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);
                await _userRepository.SaveChangesAsync();

                _logger.LogInformation("Login successful for user: {UserId}", user.UserId);

                return new AuthResponseDto
                {
                    Token = token,
                    User = userDto,
                    Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:ExpiryInHours"] ?? "24"))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", loginDto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto> RegisterAsync(CreateUserDto createUserDto)
        {
            try
            {
                _logger.LogInformation("Registration attempt for email: {Email}", createUserDto.Email);

                // Check if user already exists
                var existingUser = await _userRepository.GetByEmailAsync(createUserDto.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("Registration failed - user already exists: {Email}", createUserDto.Email);
                    throw new InvalidOperationException("User with this email already exists");
                }

                var user = _mapper.Map<User>(createUserDto);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
                user.CreatedAt = DateTime.UtcNow;

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                var userDto = _mapper.Map<UserDto>(user);
                var token = GenerateJwtToken(userDto);

                _logger.LogInformation("Registration successful for user: {UserId}", user.UserId);

                return new AuthResponseDto
                {
                    Token = token,
                    User = userDto,
                    Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["JWT:ExpiryInHours"] ?? "24"))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", createUserDto.Email);
                throw;
            }
        }

        public string GenerateJwtToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JWT");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("CompanyId", user.CompanyId?.ToString() ?? string.Empty)

            };

            // Add role claims
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // Add permission claims
            foreach (var permission in user.Permissions)
            {
                claims.Add(new Claim("permission", permission.Name));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(int.Parse(jwtSettings["ExpiryInHours"] ?? "24")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JWT");
                var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<UserDto> GetCurrentUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetUserWithRolesAsync(userId);
                if (user == null) return null;
                user.UserPermissions = (await _userRepository.GetUserPermissionsAsync(userId)).ToList();
                user.UserSubscription = (await _userRepository.GetUserWithSubscriptionAsync(userId))?.UserSubscription;
                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user: {UserId}", userId);
                throw;
            }
        }

        public async Task LogoutAsync(int userId)
        {
            try
            {
                _logger.LogInformation("User logout: {UserId}", userId);
                // Implement token blacklisting if needed
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout for user: {UserId}", userId);
                throw;
            }
        }
    }
}
