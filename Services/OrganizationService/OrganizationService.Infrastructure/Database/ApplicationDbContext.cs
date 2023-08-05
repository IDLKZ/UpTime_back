using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OrganizationService.Domain.Models;

namespace OrganizationService.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // modelBuilder.Entity<GenderModel>().HasQueryFilter(x => x.IsDeleted == false);
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["CreatedAt"] = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        entry.CurrentValues["DeletedAt"] = DateTime.UtcNow;
                        break;
                }
            }
        }


        public DbSet<AreaModel> Areas { get; set; }
        public DbSet<LegalFormModel> LegalForms { get; set; }
        public DbSet<InfoTypeModel> InfoTypes { get; set; }
        public DbSet<OrganizationModel> Organizations { get; set; }
        public DbSet<BranchModel> Branches { get; set; }
        public DbSet<InfoModel> Infos { get; set; }
        public DbSet<UserOrganizationModel> UserOrganizations { get; set; }
    }
}
