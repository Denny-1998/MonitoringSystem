using Microsoft.EntityFrameworkCore;
using LoggingService.Models;
using LoggingService;
using LoggingService.LoggingHandler;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Ensure database is created
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<MonitoringDbContext>();
            context.Database.EnsureCreated(); // Creates the database and tables if they don't exist
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<MonitoringDbContext>(options =>
                    options.UseSqlite("Data Source=monitoring.db")); // SQLite database file

                services.AddTransient<JsonExtractor>();
                services.AddTransient<LoggingStorageHandler>();


                services.AddHostedService<Worker>();
            });
}
