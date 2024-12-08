using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using LoggingService.LoggingHandler;
using LoggingService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace LoggingServiceTest
{
    public class JsonExtractorTests
    {

        private readonly Mock<ILogger<LoggingStorageHandler>> _mockLogger;
        private readonly JsonExtractor _jsonExtractor;

        public JsonExtractorTests()
        {
            _mockLogger = new Mock<ILogger<LoggingStorageHandler>>();
            _jsonExtractor = new JsonExtractor(_mockLogger.Object);
        }

        [Fact]
        public void ExtractLogEntry_ValidJson_ReturnsLogEntry()
        {
            // Arrange
            var validJson = @"{
            'Message': 'Test message',
            'Level': 1,
            'Timestamp': '2024-01-01T12:00:00',
            'Source': 'TestSource',
            'UserId': 'user123'
        }";

            // Act
            var result = _jsonExtractor.ExtractLogEntry(validJson);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test message", result.Message);
            Assert.Equal(LoggingService.Models.LogLevel.Debug, result.Level);
            Assert.Equal("TestSource", result.Source);
            Assert.Equal("user123", result.UserId);
        }

        [Fact]
        public void ExtractLogEntry_InvalidJson_ReturnsNull()
        {
            // Arrange
            var invalidJson = "{ invalid json }";

            // Act
            var result = _jsonExtractor.ExtractLogEntry(invalidJson);

            // Assert
            Assert.Null(result);
        }
    }
}
