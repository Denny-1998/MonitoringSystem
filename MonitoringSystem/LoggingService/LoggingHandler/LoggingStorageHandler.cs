using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingService.Models;

namespace LoggingService.LoggingHandler
{
    public class LoggingStorageHandler
    {
        private readonly MonitoringDbContext _dbContext;
        private readonly JsonExtractor _jsonExtractor;
        private readonly ILogger<LoggingStorageHandler> _logger;

        public LoggingStorageHandler(MonitoringDbContext dbContext, ILogger<LoggingStorageHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;

            _jsonExtractor = new JsonExtractor(_logger);
        }

        public void HandleLogMessage(string message)
        {
            LogEntry logEntry = _jsonExtractor.ExtractLogEntry(message);

            _dbContext.LogEntries.AddAsync(logEntry);
            _dbContext.SaveChangesAsync();
            
            _logger.LogInformation("Stored LogEntry: {@LogEntry}", logEntry.Message); 


        }
    }
}
