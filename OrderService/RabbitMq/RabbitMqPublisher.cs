using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using RabbitMQ.Client;

namespace OrderService.RabbitMq
{
    /// <summary>
    /// RabbitmQ class to publish details
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RabbitMqPublisher<T>
    {
        /// <summary>
        /// Declare property
        /// </summary>
        private readonly string _exchangeName = "amq.direct";
        private readonly string _routingKey = "test-direct";
        ConnectionFactory factory = new ConnectionFactory();

        /// <summary>
        /// Constructor
        /// </summary>
        public RabbitMqPublisher()
        {

            Uri uri = new Uri("");
            factory.Ssl.Enabled = true;
            factory.Uri = uri;
            factory.UserName = "";
            factory.Password = "";

        }

        /// <summary>
        /// This is publish method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Publish(T message, string key = null)
        {
            try
            {
                var ms = new MemoryStream();
                var writer = new BsonDataWriter(ms);
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, message);
                var bytes = ms.ToArray();

                using (IConnection connection = factory.CreateConnection())
                {
                    using (IModel channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct, durable: true);
                        channel.BasicPublish(exchange: _exchangeName, routingKey:
                            _routingKey, basicProperties: null, body: bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to publish event to message broker");
            }

        }
    }
}
