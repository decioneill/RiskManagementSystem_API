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
            options.UseSqlServer(Configuration.GetConnectionString("Users"));
        }

        public DbSet<User> Users { get; set; }
    }
}