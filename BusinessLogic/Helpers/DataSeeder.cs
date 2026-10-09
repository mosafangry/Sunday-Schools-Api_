using BCrypt.Net;
using DataAccess.DatabaseContext;
using DataAccess.Entities.AuthEntities;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Helpers
{
    public class DataSeeder
    {
        private readonly PharmaDbContext _dbContext;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(
            PharmaDbContext dbContext,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            ISubscriptionRepository subscriptionRepository,
            IUserRepository userRepository,
            ILogger<DataSeeder> logger)
        {
            _dbContext = dbContext;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _subscriptionRepository = subscriptionRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task SeedDataAsync()
        {
            try
            {
                _logger.LogInformation("Starting data seeding process.");

                // Ensure database is created
                await _dbContext.Database.EnsureCreatedAsync();

                // Clear Subscriptions table to avoid conflicts
                _logger.LogInformation("Clearing Subscriptions table.");
                await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Subscriptions");

                // Seed Subscriptions
                _logger.LogInformation("Seeding subscriptions.");
                using var subscriptionTransaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Subscriptions ON");
                    var subscriptions = new[]
                    {
                        new Subscription { SubscriptionId = 1, Name = "Reports", Description = "Basic plan", Price = 9.99m, DurationInDays = 100 },
                        new Subscription { SubscriptionId = 2, Name = "Premium", Description = "Premium plan", Price = 19.99m, DurationInDays = 100 },
                        new Subscription { SubscriptionId = 3, Name = "Enterprise", Description = "Enterprise plan", Price = 49.99m, DurationInDays = 29 }
                    };
                    foreach (var subscription in subscriptions)
                    {
                        _logger.LogInformation("Adding subscription: {SubscriptionId}, {Name}", subscription.SubscriptionId, subscription.Name);
                        _dbContext.Subscriptions.Add(subscription); // Directly add to context
                    }
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Subscriptions OFF");
                    await subscriptionTransaction.CommitAsync();
                    _logger.LogInformation("Subscriptions seeded successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while seeding subscriptions.");
                    await subscriptionTransaction.RollbackAsync();
                    throw;
                }

                // Verify Subscriptions exist
                var subscriptionIds = await _dbContext.Subscriptions.Select(s => s.SubscriptionId).ToListAsync();
                _logger.LogInformation("Subscriptions table contains IDs: {SubscriptionIds}", string.Join(", ", subscriptionIds));
                if (!subscriptionIds.Contains(1) || !subscriptionIds.Contains(2) || !subscriptionIds.Contains(3))
                {
                    _logger.LogError("Required subscriptions (IDs 1, 2, 3) not found in Subscriptions table. Found: {SubscriptionIds}", string.Join(", ", subscriptionIds));
                    throw new InvalidOperationException("Cannot proceed: Required subscriptions are missing.");
                }

                // Seed Roles
                if (!await _dbContext.Roles.AnyAsync())
                {
                    _logger.LogInformation("Seeding roles.");
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles ON");
                        var roles = new[]
                        {
                            new Role { RoleId = 1, Name = "Admin", Description = "Administrator with full access" },
                            new Role { RoleId = 2, Name = "User", Description = "Regular user" },
                            new Role { RoleId = 3, Name = "Premium", Description = "Premium user with extended access" },
                            new Role { RoleId = 4, Name = "CompanyRepresentative", Description = "Company representative with access to reports, drugs, drug reports, and literature reviews" }
                        };
                        foreach (var role in roles)
                        {
                            await _roleRepository.AddAsync(role);
                        }
                        await _dbContext.SaveChangesAsync();
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Roles OFF");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while seeding roles.");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Seed Permissions
                if (!await _dbContext.Permissions.AnyAsync())
                {
                    _logger.LogInformation("Seeding permissions.");
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Permissions ON");
                        var permissions = new[]
                        {
                            new Permission { PermissionId = 1, Name = "FileUpload", Description = "Upload files", Category = "File" },
                            new Permission { PermissionId = 2, Name = "FileDownload", Description = "Download files", Category = "File" },
                            new Permission { PermissionId = 3, Name = "ViewReports", Description = "View reports", Category = "Report" },
                            new Permission { PermissionId = 4, Name = "CreateReports", Description = "Create reports", Category = "Report" },
                            new Permission { PermissionId = 5, Name = "UserManagement", Description = "Manage users", Category = "Admin" },
                            new Permission { PermissionId = 6, Name = "SystemSettings", Description = "Manage system settings", Category = "Admin" },
                            new Permission { PermissionId = 7, Name = "UpdateReports", Description = "Update reports", Category = "Report" },
                            new Permission { PermissionId = 8, Name = "DeleteReports", Description = "Delete reports", Category = "Report" },
                            new Permission { PermissionId = 9, Name = "ViewDrugs", Description = "View drugs", Category = "Drug" },
                            new Permission { PermissionId = 10, Name = "CreateDrugs", Description = "Create drugs", Category = "Drug" },
                            new Permission { PermissionId = 11, Name = "UpdateDrugs", Description = "Update drugs", Category = "Drug" },
                            new Permission { PermissionId = 12, Name = "DeleteDrugs", Description = "Delete drugs", Category = "Drug" },
                            new Permission { PermissionId = 13, Name = "ViewDrugReports", Description = "View drug reports", Category = "DrugReport" },
                            new Permission { PermissionId = 14, Name = "CreateDrugReports", Description = "Create drug reports", Category = "DrugReport" },
                            new Permission { PermissionId = 15, Name = "UpdateDrugReports", Description = "Update drug reports", Category = "DrugReport" },
                            new Permission { PermissionId = 16, Name = "DeleteDrugReports", Description = "Delete drug reports", Category = "DrugReport" },
                            new Permission { PermissionId = 17, Name = "ViewLiteratureReviews", Description = "View literature reviews", Category = "Volunteer" },
                            new Permission { PermissionId = 18, Name = "CreateLiteratureReviews", Description = "Create literature reviews", Category = "Volunteer" },
                            new Permission { PermissionId = 19, Name = "UpdateLiteratureReviews", Description = "Update literature reviews", Category = "Volunteer" },
                            new Permission { PermissionId = 20, Name = "DeleteLiteratureReviews", Description = "Delete literature reviews", Category = "Volunteer" }
                        };
                        foreach (var permission in permissions)
                        {
                            await _permissionRepository.AddAsync(permission);
                        }
                        await _dbContext.SaveChangesAsync();
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Permissions OFF");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while seeding permissions.");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Verify Permissions exist
                var permissionIds = await _dbContext.Permissions.Select(p => p.PermissionId).ToListAsync();
                _logger.LogInformation("Permissions table contains IDs: {PermissionIds}", string.Join(", ", permissionIds));
                if (!permissionIds.Contains(1) || !permissionIds.Contains(2) || !permissionIds.Contains(3) || !permissionIds.Contains(4) || !permissionIds.Contains(5))
                {
                    _logger.LogError("Required permissions (IDs 1, 2, 3, 4, 5) not found in Permissions table. Found: {PermissionIds}", string.Join(", ", permissionIds));
                    throw new InvalidOperationException("Cannot seed SubscriptionPermissions: Required permissions are missing.");
                }

                // Seed Admin User
                if (!await _dbContext.Users.AnyAsync(u => u.UserId == 1))
                {
                    _logger.LogInformation("Seeding admin user.");
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users ON");
                        var existingUser = await _dbContext.Users
                            .Include(u => u.UserRoles)
                            .FirstOrDefaultAsync(u => u.UserId == 1);
                        if (existingUser != null)
                        {
                            _dbContext.UserRoles.RemoveRange(existingUser.UserRoles);
                            _dbContext.Users.Remove(existingUser);
                            await _dbContext.SaveChangesAsync();
                        }

                        var adminUser = new User
                        {
                            UserId = 1,
                            Username = "admin",
                            Email = "admin@example.com",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                            FirstName = "Admin",
                            LastName = "User",
                            PhoneNumber = "123-456-7890",
                            IsActive = true,
                            CreatedAt = DateTime.UtcNow,
                            CompanyId = null,
                            UserRoles = new List<UserRole>
                            {
                                new UserRole { UserId = 1, RoleId = 1 }
                            }
                        };
                        await _userRepository.AddAsync(adminUser);
                        await _dbContext.SaveChangesAsync();
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Users OFF");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while seeding admin user.");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Seed Role Permissions
                if (!await _dbContext.RolePermissions.AnyAsync())
                {
                    _logger.LogInformation("Seeding role permissions.");
                    using var transaction = await _dbContext.Database.BeginTransactionAsync();
                    try
                    {
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RolePermissions ON");
                        var rolePermissions = new[]
                        {
                            new RolePermission { RolePermissionId = 1, RoleId = 1, PermissionId = 1 },
                            new RolePermission { RolePermissionId = 2, RoleId = 1, PermissionId = 2 },
                            new RolePermission { RolePermissionId = 3, RoleId = 1, PermissionId = 3 },
                            new RolePermission { RolePermissionId = 4, RoleId = 1, PermissionId = 4 },
                            new RolePermission { RolePermissionId = 5, RoleId = 1, PermissionId = 5 },
                            new RolePermission { RolePermissionId = 6, RoleId = 1, PermissionId = 6 },
                            new RolePermission { RolePermissionId = 7, RoleId = 1, PermissionId = 7 },
                            new RolePermission { RolePermissionId = 8, RoleId = 1, PermissionId = 8 },
                            new RolePermission { RolePermissionId = 9, RoleId = 1, PermissionId = 9 },
                            new RolePermission { RolePermissionId = 10, RoleId = 1, PermissionId = 10 },
                            new RolePermission { RolePermissionId = 11, RoleId = 1, PermissionId = 11 },
                            new RolePermission { RolePermissionId = 12, RoleId = 1, PermissionId = 12 },
                            new RolePermission { RolePermissionId = 13, RoleId = 1, PermissionId = 13 },
                            new RolePermission { RolePermissionId = 14, RoleId = 1, PermissionId = 14 },
                            new RolePermission { RolePermissionId = 15, RoleId = 1, PermissionId = 15 },
                            new RolePermission { RolePermissionId = 16, RoleId = 1, PermissionId = 16 },
                            new RolePermission { RolePermissionId = 17, RoleId = 1, PermissionId = 17 },
                            new RolePermission { RolePermissionId = 18, RoleId = 1, PermissionId = 18 },
                            new RolePermission { RolePermissionId = 19, RoleId = 1, PermissionId = 19 },
                            new RolePermission { RolePermissionId = 20, RoleId = 1, PermissionId = 20 },
                            new RolePermission { RolePermissionId = 21, RoleId = 2, PermissionId = 3 },
                            new RolePermission { RolePermissionId = 22, RoleId = 3, PermissionId = 1 },
                            new RolePermission { RolePermissionId = 23, RoleId = 3, PermissionId = 2 },
                            new RolePermission { RolePermissionId = 24, RoleId = 3, PermissionId = 3 },
                            new RolePermission { RolePermissionId = 25, RoleId = 3, PermissionId = 4 },
                            new RolePermission { RolePermissionId = 26, RoleId = 4, PermissionId = 3 },
                            new RolePermission { RolePermissionId = 27, RoleId = 4, PermissionId = 4 },
                            new RolePermission { RolePermissionId = 28, RoleId = 4, PermissionId = 7 },
                            new RolePermission { RolePermissionId = 29, RoleId = 4, PermissionId = 8 },
                            new RolePermission { RolePermissionId = 30, RoleId = 4, PermissionId = 9 },
                            new RolePermission { RolePermissionId = 31, RoleId = 4, PermissionId = 10 },
                            new RolePermission { RolePermissionId = 32, RoleId = 4, PermissionId = 11 },
                            new RolePermission { RolePermissionId = 33, RoleId = 4, PermissionId = 12 },
                            new RolePermission { RolePermissionId = 34, RoleId = 4, PermissionId = 13 },
                            new RolePermission { RolePermissionId = 35, RoleId = 4, PermissionId = 14 },
                            new RolePermission { RolePermissionId = 36, RoleId = 4, PermissionId = 15 },
                            new RolePermission { RolePermissionId = 37, RoleId = 4, PermissionId = 16 },
                            new RolePermission { RolePermissionId = 38, RoleId = 4, PermissionId = 17 },
                            new RolePermission { RolePermissionId = 39, RoleId = 4, PermissionId = 18 },
                            new RolePermission { RolePermissionId = 40, RoleId = 4, PermissionId = 19 },
                            new RolePermission { RolePermissionId = 41, RoleId = 4, PermissionId = 20 }
                        };
                        _dbContext.RolePermissions.AddRange(rolePermissions);
                        await _dbContext.SaveChangesAsync();
                        await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT RolePermissions OFF");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while seeding role permissions.");
                        await transaction.RollbackAsync();
                        throw;
                    }
                }

                // Seed Subscription Permissions
                _logger.LogInformation("Seeding subscription permissions.");
                using var subscriptionPermTransaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SubscriptionPermissions ON");
                    var subscriptionPermissions = new[]
                    {
                        new SubscriptionPermission { SubscriptionPermissionId = 1, SubscriptionId = 1, PermissionId = 3 },
                        new SubscriptionPermission { SubscriptionPermissionId = 2, SubscriptionId = 2, PermissionId = 1 },
                        new SubscriptionPermission { SubscriptionPermissionId = 3, SubscriptionId = 2, PermissionId = 2 },
                        new SubscriptionPermission { SubscriptionPermissionId = 4, SubscriptionId = 2, PermissionId = 3 },
                        new SubscriptionPermission { SubscriptionPermissionId = 5, SubscriptionId = 2, PermissionId = 4 },
                        new SubscriptionPermission { SubscriptionPermissionId = 6, SubscriptionId = 3, PermissionId = 1 },
                        new SubscriptionPermission { SubscriptionPermissionId = 7, SubscriptionId = 3, PermissionId = 2 },
                        new SubscriptionPermission { SubscriptionPermissionId = 8, SubscriptionId = 3, PermissionId = 3 },
                        new SubscriptionPermission { SubscriptionPermissionId = 9, SubscriptionId = 3, PermissionId = 4 },
                        new SubscriptionPermission { SubscriptionPermissionId = 10, SubscriptionId = 3, PermissionId = 5 }
                    };
                    foreach (var permission in subscriptionPermissions)
                    {
                        _logger.LogInformation("Adding subscription permission: SubscriptionPermissionId={Id}, SubscriptionId={SubscriptionId}, PermissionId={PermissionId}",
                            permission.SubscriptionPermissionId, permission.SubscriptionId, permission.PermissionId);
                        _dbContext.SubscriptionPermissions.Add(permission);
                    }
                    await _dbContext.SaveChangesAsync();
                    await _dbContext.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT SubscriptionPermissions OFF");
                    await subscriptionPermTransaction.CommitAsync();
                    _logger.LogInformation("Subscription permissions seeded successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while seeding subscription permissions.");
                    await subscriptionPermTransaction.RollbackAsync();
                    throw;
                }

                _logger.LogInformation("Data seeding completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during data seeding.");
                throw;
            }
        }
    }
}