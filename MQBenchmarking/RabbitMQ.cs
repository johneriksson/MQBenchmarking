using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQBenchmarking {
    class RabbitMQ : MessageQueue {
        int numMessages = 0, messageSize = 0;
        IConnection connection = null;
        IModel channel = null;
        string queueName = "myQueueName";
        IBasicProperties basicProperties = null;

        public void Setup(int numMessages, int messageSize){
            this.numMessages = numMessages;
            this.messageSize = messageSize;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            basicProperties = channel.CreateBasicProperties();
            basicProperties.Persistent = true;
            basicProperties.DeliveryMode = 2;
        }

        public void Teardown() {
            channel.QueueDelete(queueName);
            channel.Close();
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
