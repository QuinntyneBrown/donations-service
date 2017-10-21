using DonationsService.Data.Helpers;
using DonationsService.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace DonationsService.Data
{
    public interface IDonationsServiceContext
    {
        DbSet<Tenant> Tenants { get; set; }        
        DbSet<Donation> Donations { get; set; }
        DbSet<Donor> Donors { get; set; }
        Task<int> SaveChangesAsync(string username = null);
    }
    
    public class DonationsServiceContext: DbContext, IDonationsServiceContext
    {
        public DonationsServiceContext()
            :base("DonationsServiceContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = true;
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Donor> Donors { get; set; }

        public int SaveChanges(string username)
        {
            UpdateLoggableEntries(username);
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync(string username)
        {
            UpdateLoggableEntries(username);
            return base.SaveChangesAsync();
        }

        public override int SaveChanges() => this.SaveChanges(null);

        public override Task<int> SaveChangesAsync() => this.SaveChangesAsync(null);

        public void UpdateLoggableEntries(string username = null)
        {
            foreach (var entity in ChangeTracker.Entries()
                .Where(e => e.Entity is ILoggable && ((e.State == EntityState.Added || (e.State == EntityState.Modified))))
                .Select(x => x.Entity as ILoggable))
            {
                var isNew = entity.CreatedOn == default(DateTime);
                entity.CreatedOn = isNew ? DateTime.UtcNow : entity.CreatedOn;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.CreatedBy = isNew ? username : entity.CreatedBy;
                entity.LastModifiedBy = username;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var convention = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
                "SoftDeleteColumnName",
                (type, attributes) => attributes.Single().ColumnName);

            modelBuilder.Conventions.Add(convention);
        }
    }
}