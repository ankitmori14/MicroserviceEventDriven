using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService
{
    /// <summary>
    /// rabbitMq Subscriber
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RabbitMqSubscriber<T> : IHostedService
    {
        /// <summary>
        /// Property Declare
        /// </summary>
        private readonly string _exchangeName = "amq.direct";
        private readonly string _routingKey = "test-direct";
        private readonly string _queueName = "test-Queue";

        /// <summary>
        /// Start method
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory();
            Uri uri = new Uri("");
            factory.Ssl.Enabled = true;
            factory.Uri = uri;
            factory.UserName = "";
            factory.Password = "";

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            string routingKey = _routingKey;

            var exchangeNameTopic = _exchangeName;

            var queueName = _queueName;
            channel.QueueDeclare(queueName, true, false, false, null);
            channel.QueueBind(queueName, exchangeNameTopic, routingKey);


            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();


                using (var reader = new BsonDataReader(new MemoryStream(body)))
                {
                    var serializer = new JsonSerializer();
                    var evt = serializer.Deserialize<T>(reader);
                    SendEmailNotification sendEmailNotification = new SendEmailNotification();
                    sendEmailNotification.SendEmail(evt);
                }

            };
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop Method
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
