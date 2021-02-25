using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RiskManagementSystem_API.Entities;

namespace RiskManagementSystem_API.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("RiskManagement"));
        }

        //Override in order to allow for composite keys
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MitigationRisk>().HasKey(mr => new { mr.MitigationId, mr.RiskId });
            modelBuilder.Entity<RiskOwner>().HasKey(ro => new { ro.RiskId, ro.UserId });
            modelBuilder.Entity<RiskProperty>().HasKey(rp => new { rp.RiskId, rp.PropertyId});
            modelBuilder.Entity<TeamMember>().HasKey(tm => new { tm.ProjectId, tm.UserId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<RiskOwner> RiskOwners { get; set; }
        public DbSet<RiskStatusHistory> RiskStatusHistories { get; set; }
        public DbSet<RiskProperty> RiskProperties { get; set; }
        public DbSet<Mitigation> Mitigations { get; set; }
        public DbSet<MitigationRisk> MitigationRisks { get; set; }
    }
}