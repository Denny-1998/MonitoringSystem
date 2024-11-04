using LoggingService.LoggingHandler;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace LoggingService.MessageSubscriber
{
    public class MessageConsumer
    {

        private readonly string _hostname = "rabbitmq";  
        private readonly string _queueName = "LogQueue"; 
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly LoggingStorageHandler _loggingStorageHandler;

        public MessageConsumer(LoggingStorageHandler loggingStorageHandler)
        {
            _loggingStorageHandler = loggingStorageHandler;
            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize JSON and process message
                ProcessMessage(message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        private void ProcessMessage(string message)
        {
            _loggingStorageHandler.HandleLogMessage(message);
            Console.WriteLine("Received message: " + message);
        }
    }
}
