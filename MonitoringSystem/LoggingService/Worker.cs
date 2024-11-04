using LoggingService.LoggingHandler;
using LoggingService.MessageSubscriber;
using LoggingService.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace LoggingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider; 
        private readonly MessageConsumer _consumer;
        private readonly LoggingStorageHandler _loggingStorageHandler;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) 
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>();

                var loggingHandlerLogger = scope.ServiceProvider.GetRequiredService<ILogger<LoggingStorageHandler>>();
                _loggingStorageHandler = new LoggingStorageHandler(dbcontext, loggingHandlerLogger);
            }

            _consumer = new MessageConsumer(_loggingStorageHandler);

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Delay(5000);



            _consumer.StartListening();
            _logger.LogInformation("MessageConsumer is listening for messages");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    using (var scope = _serviceProvider.CreateScope()) 
            //    {
            //        var dbContext = scope.ServiceProvider.GetRequiredService<MonitoringDbContext>(); 

            //        if (_logger.IsEnabled(Microsoft.Extensions.Logging.LogLevel.Information))
            //        {
            //            var logEntry = new LogEntry
            //            {
            //                Timestamp = DateTime.UtcNow,
            //                Message = "Worker is running",
            //                Level = Models.LogLevel.Information,
            //                Source = "LoggingService.Worker",
            //                UserId = "TestUser",
            //                CorrelationId = Guid.NewGuid().ToString(),
            //                TraceId = Guid.NewGuid().ToString(),
            //                SpanId = Guid.NewGuid().ToString(),
            //                Exception = string.Empty
            //            };

            //            await dbContext.LogEntries.AddAsync(logEntry);
            //            await dbContext.SaveChangesAsync();

            //            _logger.LogInformation("Log entry added: {message}", logEntry.Message);
            //        }
            //    }
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}
