using DataAccess.Entities;
using DataAccess.Entities.AuthEntities;
using DataAccess.Entities.FdaValidation;
using DataAccess.Entities.LiteratureReviewEntities;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Security.Claims;

namespace DataAccess.DatabaseContext
{
    public class PharmaDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int _userId;
        private readonly int _companyId;

        public PharmaDbContext(DbContextOptions<PharmaDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
                _userId = userId;
            else
                _userId = 1;
            // 🔽 Add this block for CompanyId
            var companyIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("CompanyId")?.Value;
            if (int.TryParse(companyIdClaim, out var companyId))
                _companyId = companyId;
            else
                _companyId = 1; // or throw if company is required

        }
        //report
        public DbSet<IcsrEntity> IcsrReports { get; set; }
        public DbSet<MessageHeaderEntity> MessageHeaders { get; set; }
        public DbSet<PrimarySourceEntity> PrimarySources { get; set; }
        public DbSet<SenderEntity> Senders { get; set; }
        public DbSet<ReceiverEntity> Receivers { get; set; }
        public DbSet<PatientEntity> Patients { get; set; }
        public DbSet<ReactionEntity> Reactions { get; set; }
        public DbSet<DrugEntity> Drugs { get; set; }
        public DbSet<ReportMedication> ReportMedications { get; set; }
        public DbSet<ActiveSubstanceEntity> ActiveSubstances { get; set; }
        public DbSet<DrugReactionRelatednessEntity> DrugReactionRelatedness { get; set; }
        public DbSet<SummaryEntity> Summaries { get; set; }
        public DbSet<ConcomitantMedicationEntity> ConcomitantMedications { get; set; }
        public DbSet<ConcomitantDiseaseEntity> ConcomitantDiseases { get; set; }
        //FDA VAlidation
        public DbSet<FDAValidationResult> FDAValidationResults { get; set; }
        public DbSet<FDAValidationErrors> FDAValidationErrors { get; set; }
        //Auth and subscrptions entites
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<SubscriptionPermission> SubscriptionPermissions { get; set; }
        //

        public DbSet<Company> Companies { get; set; }

        // ========== LiteratureReviews ==========
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<LiteratureReview> LiteratureReviews { get; set; }
        public DbSet<LiteratureReviewMedication> LiteratureReviewMedications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Publisher → Journal (One-to-Many)
            modelBuilder.Entity<Journal>()
                .HasOne(j => j.Publisher)
                .WithMany(p => p.Journals)
                .HasForeignKey(j => j.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Journal → LiteratureReview (One-to-Many)
            modelBuilder.Entity<LiteratureReview>()
                .HasOne(lr => lr.Journal)
                .WithMany(j => j.LiteratureReviews)
                .HasForeignKey(lr => lr.JournalId)
                .OnDelete(DeleteBehavior.Cascade);

            // LiteratureReview ↔ Medication (Many-to-Many)
            modelBuilder.Entity<LiteratureReviewMedication>()
                .HasKey(x => new { x.LiteratureReviewId, x.MedicationId });

            modelBuilder.Entity<LiteratureReviewMedication>()
                .HasOne(x => x.LiteratureReview)
                .WithMany(lr => lr.LiteratureReviewMedications)
                .HasForeignKey(x => x.LiteratureReviewId);

            modelBuilder.Entity<LiteratureReviewMedication>()
                .HasOne(x => x.Medication)
                .WithMany(m => m.LiteratureReviewMedications)
                .HasForeignKey(x => x.MedicationId);

            // IcsrEntity relationships
            modelBuilder.Entity<IcsrEntity>()
                .HasOne(i => i.MessageHeader)
                .WithOne(m => m.Icsr)
                .HasForeignKey<IcsrEntity>(i => i.MessageHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IcsrEntity>()
                .HasOne(i => i.PrimarySource)
                .WithOne(p => p.Icsr)
                .HasForeignKey<IcsrEntity>(i => i.PrimarySourceId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IcsrEntity>()
                .HasOne(i => i.Sender)
                .WithOne(s => s.Icsr)
                .HasForeignKey<IcsrEntity>(i => i.SenderId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IcsrEntity>()
                .HasOne(i => i.Receiver)
                .WithOne(r => r.Icsr)
                .HasForeignKey<IcsrEntity>(i => i.ReceiverId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IcsrEntity>()
                .HasOne(i => i.Patient)
                .WithOne(p => p.Icsr)
                .HasForeignKey<IcsrEntity>(i => i.PatientId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IcsrEntity>()
                .HasMany(i => i.ConcomitantMedications)
                .WithOne(c => c.Icsr)
                .HasForeignKey(c => c.IcsrId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IcsrEntity>()
                .HasMany(i => i.ConcomitantDiseases)
                .WithOne(c => c.Icsr)
                .HasForeignKey(c => c.IcsrId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient relationships
            modelBuilder.Entity<PatientEntity>()
                .HasMany(p => p.Reactions)
                .WithOne(r => r.Patient)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<PatientEntity>()
            //    .HasMany(p => p.Drugs)
            //    .WithOne(d => d.Patient)
            //    .HasForeignKey(d => d.PatientId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PatientEntity>()
                .HasOne(p => p.Summary)
                .WithOne(s => s.Patient)
                .HasForeignKey<SummaryEntity>(s => s.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Drug relationships
            modelBuilder.Entity<DrugEntity>()
                .HasOne(d => d.ActiveSubstance)
                .WithOne(a => a.Drug)
                .HasForeignKey<ActiveSubstanceEntity>(a => a.DrugId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DrugEntity>()
                .HasMany(d => d.DrugReactionRelatedness)
                .WithOne(d => d.Drug)
                .HasForeignKey(d => d.DrugId)
                .OnDelete(DeleteBehavior.Cascade);
       

            // Configure table and column names
            modelBuilder.Entity<IcsrEntity>().ToTable("IcsrReports");
            modelBuilder.Entity<MessageHeaderEntity>().ToTable("MessageHeaders");
            modelBuilder.Entity<PrimarySourceEntity>().ToTable("PrimarySources");
            modelBuilder.Entity<SenderEntity>().ToTable("Senders");
            modelBuilder.Entity<ReceiverEntity>().ToTable("Receivers");
            modelBuilder.Entity<PatientEntity>().ToTable("Patients");
            modelBuilder.Entity<ReactionEntity>().ToTable("Reactions");
            modelBuilder.Entity<DrugEntity>().ToTable("Drugs");
            modelBuilder.Entity<ActiveSubstanceEntity>().ToTable("ActiveSubstances");
            modelBuilder.Entity<DrugReactionRelatednessEntity>().ToTable("DrugReactionRelatedness");
            modelBuilder.Entity<SummaryEntity>().ToTable("Summaries");
            modelBuilder.Entity<ConcomitantMedicationEntity>().ToTable("ConcomitantMedications");
            modelBuilder.Entity<ConcomitantDiseaseEntity>().ToTable("ConcomitantDiseases");

            // Configure indexes
            //modelBuilder.Entity<IcsrEntity>()
            //    .HasIndex(i => i.SafetyReportId)
            //    .IsUnique();

            //auth and subscriptions users
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // UserRole configuration
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId);

                entity.HasIndex(e => new { e.UserId, e.RoleId }).IsUnique();
            });

            // UserPermission configuration
            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasOne(up => up.User)
                    .WithMany(u => u.UserPermissions)
                    .HasForeignKey(up => up.UserId);

                entity.HasOne(up => up.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(up => up.PermissionId);

                entity.HasIndex(e => new { e.UserId, e.PermissionId }).IsUnique();
            });

            // RolePermission configuration
            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasOne(rp => rp.Role)
                    .WithMany(r => r.RolePermissions)
                    .HasForeignKey(rp => rp.RoleId);

                entity.HasOne(rp => rp.Permission)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(rp => rp.PermissionId);

                entity.HasIndex(e => new { e.RoleId, e.PermissionId }).IsUnique();
            });

            // UserSubscription configuration
            modelBuilder.Entity<UserSubscription>(entity =>
            {
                entity.HasOne(us => us.User)
                    .WithOne(u => u.UserSubscription)
                    .HasForeignKey<UserSubscription>(us => us.UserId);

                entity.HasOne(us => us.Subscription)
                    .WithMany(s => s.UserSubscriptions)
                    .HasForeignKey(us => us.SubscriptionId);
            });

            // SubscriptionPermission configuration
            modelBuilder.Entity<SubscriptionPermission>(entity =>
            {
                entity.HasOne(sp => sp.Subscription)
                    .WithMany(s => s.SubscriptionPermissions)
                    .HasForeignKey(sp => sp.SubscriptionId);

                entity.HasOne(sp => sp.Permission)
                    .WithMany()
                    .HasForeignKey(sp => sp.PermissionId);

                entity.HasIndex(e => new { e.SubscriptionId, e.PermissionId }).IsUnique();
            });
            // Apply filter to all entities implementing ITrackable
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ITrackable).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(PharmaDbContext)
                        .GetMethod(nameof(ApplyCompanyFilter), BindingFlags.NonPublic | BindingFlags.Instance)!
                        .MakeGenericMethod(entityType.ClrType);

                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
            // Seed data
            //SeedData(modelBuilder);

        }
        private void ApplyCompanyFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : class, ITrackable
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => e.CompanyId == _companyId);
        }
        
        public override int SaveChanges()
        {
            ApplyAuditInfo();
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInfo();
            foreach (var entry in ChangeTracker.Entries<ITrackable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedByUserId = _userId;
                    entry.Entity.CompanyId = _companyId;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedByUserId = _userId;
                    entry.Entity.CompanyId = _companyId;
                }
            }

            // return await base.SaveChangesAsync(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }
        private void ApplyAuditInfo()
        {
            var user = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
            var entries = ChangeTracker.Entries<AuditableEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = user;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = user;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = user;
                        break;
                }
            }
        }

    }
}
