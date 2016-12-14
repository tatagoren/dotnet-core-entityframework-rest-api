using CallCenter.Data.Settings;
using CallCenter.Model.Entities;
using CallCenter.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CallCenter.Data
{
    public class CallCenterContext : DbContext
    {
        private IConfigurationRoot _config;
        private DatabaseSettings _dbSettings;

        public CallCenterContext(IOptions<DatabaseSettings> dbOptions,DbContextOptions options) : base(options)
        {
            _dbSettings = dbOptions.Value;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Call> Calls { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DB_CallCenter;Trusted_Connection=true;MultipleActiveResultSets=true;");
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}