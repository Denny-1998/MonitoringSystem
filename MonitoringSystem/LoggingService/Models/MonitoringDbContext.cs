using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace LoggingService.Models
{
    public class MonitoringDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MonitoringDbContext(DbContextOptions<MonitoringDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<LogEntry> LogEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use the connection string from appsettings.json
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("LoggingDatabase"));
            }
        }
    }

}
