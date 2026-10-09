
using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Auth;
using BusinessLogic.DTOs.Company;
using BusinessLogic.DTOs.Drugs;
using BusinessLogic.DTOs.LiteratureReviewDtos;
using BusinessLogic.DTOs.ReportMedication;
using BusinessLogic.DTOs.Subscription;
using BusinessLogic.DTOs.User;
using DataAccess.Entities;
using DataAccess.Entities.AuthEntities;
using DataAccess.Entities.LiteratureReviewEntities;
using static Dapper.SqlMapper;
namespace BusinessLogic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.UserPermissions.Select(up => up.Permission)))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.UserSubscription));

            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
 .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src =>
        src.roleId.HasValue
            ? new List<UserRole> { new UserRole { RoleId = src.roleId.Value } }
            : new List<UserRole>()
    )).ForMember(dest => dest.UserPermissions, opt => opt.Ignore())
    .ForMember(dest => dest.CompanyId, opt =>
    {
        opt.PreCondition(src => src.CompanyId.HasValue && src.CompanyId.Value != 0);
        opt.MapFrom(src => src.CompanyId);
    })
                .ForMember(dest => dest.UserSubscription, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.UserPermissions, opt => opt.Ignore())
                .ForMember(dest => dest.UserSubscription, opt => opt.Ignore());

            // Role mappings
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)));

            CreateMap<CreateRoleDto, Role>()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            CreateMap<UpdateRoleDto, Role>()
                .ForMember(dest => dest.RoleId, opt => opt.Ignore())
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            // Permission mappings
            CreateMap<Permission, PermissionDto>();
            CreateMap<CreatePermissionDto, Permission>()
                .ForMember(dest => dest.UserPermissions, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            CreateMap<UpdatePermissionDto, Permission>()
                .ForMember(dest => dest.PermissionId, opt => opt.Ignore())
                .ForMember(dest => dest.UserPermissions, opt => opt.Ignore())
                .ForMember(dest => dest.RolePermissions, opt => opt.Ignore());

            // Subscription mappings
            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.SubscriptionPermissions.Select(sp => sp.Permission)));

            CreateMap<CreateSubscriptionDto, Subscription>()
                .ForMember(dest => dest.UserSubscriptions, opt => opt.Ignore())
                .ForMember(dest => dest.SubscriptionPermissions, opt => opt.Ignore());

            CreateMap<UpdateSubscriptionDto, Subscription>()
                .ForMember(dest => dest.SubscriptionId, opt => opt.Ignore())
                .ForMember(dest => dest.UserSubscriptions, opt => opt.Ignore())
                .ForMember(dest => dest.SubscriptionPermissions, opt => opt.Ignore());

            // UserSubscription mappings
            CreateMap<UserSubscription, UserSubscriptionDto>()
                .ForMember(dest => dest.SubscriptionName, opt => opt.MapFrom(src => src.Subscription.Name));

            // UserPermission mappings
            CreateMap<UserPermission, UserPermissionDto>()
                .ForMember(dest => dest.PermissionName, opt => opt.MapFrom(src => src.Permission.Name));


            // Company mappings
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<CompanyUpdateDto, Company>();
            CreateMap<Company, CompanyUpdateDto>();

            //Drugs
            CreateMap<DrugEntity, DrugDto>();
            CreateMap<DrugEntity, DrugSummaryDto>();

            // DTO to Entity mappings
            CreateMap<DrugDto, DrugEntity>();
            CreateMap<CreateDrugDto, DrugEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Patients, opt => opt.Ignore()) // ✅ updated from `Patient` to `Patients`
                .ForMember(dest => dest.ActiveSubstance, opt => opt.Ignore())
                .ForMember(dest => dest.DrugReactionRelatedness, opt => opt.Ignore());

            CreateMap<UpdateDrugDto, DrugEntity>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Patients, opt => opt.Ignore())
                .ForMember(dest => dest.ActiveSubstance, opt => opt.Ignore())
                .ForMember(dest => dest.DrugReactionRelatedness, opt => opt.Ignore());

            // Related entity mappings
            CreateMap<PatientEntity, PatientSummaryDto>();
            CreateMap<ActiveSubstanceEntity, ActiveSubstanceDto>();
            CreateMap<DrugReactionRelatednessEntity, DrugReactionRelatednessDto>();

            //LR
            CreateMap<LiteratureReview, LiteratureReviewDto>()
                .ForMember(dest => dest.Journal, opt => opt.MapFrom(src => src.Journal))
                .ForMember(dest => dest.Medications, opt => opt.MapFrom(src => src.LiteratureReviewMedications.Select(lrm => lrm.Medication)));

            CreateMap<CreateLiteratureReviewDto, LiteratureReview>()
                .ForMember(dest => dest.LiteratureReviewMedications, opt => opt.MapFrom(src => src.MedicationIds.Select(id => new LiteratureReviewMedication { MedicationId = id })));

            CreateMap<Journal, JournalDto>()
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
            CreateMap<CreateJournalDto, Journal>();
            CreateMap<UpdateJournalDto, Journal>();

            CreateMap<Publisher, PublisherDto>();
            CreateMap<CreatePublisherDto, Publisher>();
            CreateMap<UpdatePublisherDto, Publisher>();

            CreateMap<Medication, MedicationDto>();
            CreateMap<CreateMedicationDto, Medication>();
            CreateMap<UpdateMedicationDto, Medication>();


            //ReportMedication
            // DTO to Entity mappings

            // For reading back (Entity → Dto)
            CreateMap<ReportMedication, ReportMedicationDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.ModifiedDate));

            CreateMap<CreateReportMedicationDto, ReportMedication>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());
            CreateMap<UpdateReportMedicationDto, ReportMedication>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore());

            // For summary projection
            CreateMap<ReportMedication, ReportMedicationSummaryDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));

        }
    }
}