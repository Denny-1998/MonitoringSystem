# MonitoringSystem
This assignment handles parts of a monitoring system that can be used to save logs for another application. 
In the following diagrams, the general structure is shown. 
The actual implementation consists of only the Logging Service with the RabbitMQ container. 

# C4 Diagrams
## level 1
![C4 lvl 1 (1)](https://github.com/user-attachments/assets/466e8775-691d-422a-a8ae-270a72ab9ab6)

## level 2
![C4 lvl 2 (2)](https://github.com/user-attachments/assets/6148a96c-f193-47cc-9618-05d4b8e48bbc)

## level 3
![C4 lvl 3 LoggingService (1)](https://github.com/user-attachments/assets/595c7560-4244-42e7-9118-ef04c8f6adde)

### Logging service

# Test
When running the containers, RabbitMQ can be reached through: `http://localhost:15672/`
There is a queue called `LogQueue` and messages need to be in this format: 
```json
{
  "Message": "Test log entry from RabbitMQ",
  "Level": "Information", // Must match one of the enum names
  "Timestamp": "2024-10-30T12:00:00Z", // Must be in a valid DateTime format
  "Source": "ManualTest",
  "UserId": "TestUser",
  "CorrelationId": "1234",
  "TraceId": "5678",
  "SpanId": "91011",
  "Exception": ""
}
```
