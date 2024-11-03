using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingService.Models
{
    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }

    public class LogEntry
    {
        public int Id { get; set; } 
        public string Message { get; set; }
        public LogLevel Level { get; set; } 
        public DateTime Timestamp { get; set; }
        public string Source { get; set; } 
        public string Exception { get; set; } 
        public string UserId { get; set; } 
        public string CorrelationId { get; set; } 
        public string TraceId { get; set; }
        public string SpanId { get; set; }
    }

}
