using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingService.Models;
using Newtonsoft.Json;

namespace LoggingService.LoggingHandler
{
    public class JsonExtractor
    {
        private readonly ILogger _logger;
        public JsonExtractor (ILogger<LoggingStorageHandler> logger)
        {
            _logger = logger;
        }

        public LogEntry ExtractLogEntry(string json)
        {
            try
            {
                var logEntry = JsonConvert.DeserializeObject<LogEntry>(json);

                if (logEntry == null)
                {
                    throw new JsonException("Deserialization returned null for the provided JSON.");
                }

                return logEntry;
            }
            catch (JsonException ex)
            {
                _logger.LogError($"Error deserializing JSON: {ex.Message}");
                return null; 
            }
        }
    }
}
