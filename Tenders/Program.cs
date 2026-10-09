using BusinessLogic.Helpers;
using BusinessLogic.IServicesBL;
using BusinessLogic.Services;
using BusinessLogic.ServicesBL;

using DataAccess.DatabaseContext;
using DataAccess.IRepository;
using DataAccess.Repositories;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessLogic.Mapping;
using BusinessLogic.DTOs.Auth;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Explicitly allow all hosts
builder.Configuration["AllowedHosts"] = "*";

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure JWT Settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

// Add Entity Framework
builder.Services.AddDbContext<PharmaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add HttpContext
builder.Services.AddHttpContextAccessor();

// Register existing repositories
builder.Services.AddScoped<IIcsrRepository, IcsrRepository>();
builder.Services.AddScoped<IScrapingRepository, ScrapingRepository>();

// Register authentication/authorization repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

// Register existing services
builder.Services.AddScoped<IIcsrService, IcsrService>();
builder.Services.AddScoped<IScraperService, ScraperService>();

// Register authentication/authorization services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
//Company
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
//drugs 
builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IDrugService, DrugService>();
//ReportMedications 
builder.Services.AddScoped<IReportMedicationRepository, ReportMedicationRepository>();
builder.Services.AddScoped<IReportMedicationsService, ReportMedicationsService>();
//MrdDRA
builder.Services.AddSingleton<IMedDRAService_GPT>(sp =>
{
    var medAsciiPath = builder.Configuration["MedDRA:MedAsciiPath"];
    return new MedDRAService(medAsciiPath);
});

//drug report 
builder.Services.AddScoped<IDrugReportRepository, DrugReportRepository>();
builder.Services.AddScoped<IDrugReportService, DrugReportService>();
// Register MedDRA configuration
builder.Services.Configure<MedDRAConfiguration>(
    builder.Configuration.GetSection("MedDRA"));
builder.Services.AddScoped<DataSeeder>();

// Register MedDRA services
builder.Services.AddScoped<IMedDRAService, MedDRABusinessService>();
//LR
builder.Services.AddScoped<ILiteratureReviewRepository, LiteratureReviewRepository>();
builder.Services.AddScoped<ILiteratureReviewService, LiteratureReviewService>();
// Add other services
builder.Services.AddMemoryCache();
builder.Services.AddLogging();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JWT");

    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

// Add Authorization
builder.Services.AddAuthorization();

// Add Controllers
//builder.Services.AddControllers()
//    .AddXmlSerializerFormatters();

//builder.Services.AddControllers(options =>
//{
//    options.ModelBinderProviders.Insert(0, new SafeXmlModelBinderProvider());
//});services.AddControllers()
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            // Configure JSON serialization options
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

// Add XML serialization support if you need both JSON and XML
builder.Services.AddMvc(options =>
{
    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pharma API",
        Version = "v1",
        Description = "API for pharmaceutical data with authentication"
    });

    // JWT Authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token in the format: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Get configuration values
var configuration = builder.Configuration;
var pageSize = configuration.GetValue<int>("PageSize");

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PharmaDbContext>();
    dbContext.Database.Migrate(); // Applies any pending migrations
    var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
    await seeder.SeedDataAsync();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pharma API v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
// Enable CORS globally before Authentication and Authorization
app.UseCors("AllowAll");

// Authentication & Authorization middleware (order matters!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();