using RabbitMQ.Client;

namespace MQBenchmarking {
    class RabbitMQ : MessageQueue {
        IConnection connection = null;
        IModel channel = null;
        string queueName = "myQueueName";
        IBasicProperties basicProperties = null;

        public void Setup(){
            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;
            basicProperties.DeliveryMode = 2;
        }

        public void Teardown() {
            if (channel != null) {
                channel.QueueDelete(queueName);
                channel.Close();
            }

            if (connection != null)
                connection.Close();
        }

        public void Receive() {
            channel.BasicGet(queueName, true);
        }

        public void Send(byte[] message) {
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: basicProperties, body: message);
        }
    }
}
