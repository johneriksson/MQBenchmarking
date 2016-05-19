using System;
using Apache.NMS;
using Apache.NMS.Util;

namespace MQBenchmarking {
    class ActiveMQ : MessageQueue {
        Uri connectUri = new Uri("activemq:tcp://localhost:61616");
        IConnectionFactory factory = null;
        IConnection connection = null;
        ISession session = null;
        IDestination destination = null;
        IMessageConsumer consumer = null;
        IMessageProducer producer = null;
        IBytesMessage request = null;

        public void Setup() {
            factory = new NMSConnectionFactory(connectUri);
            connection = factory.CreateConnection();
            session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);

            destination = SessionUtil.GetDestination(session, "queue://FOO.BAR");
            consumer = session.CreateConsumer(destination);
            producer = session.CreateProducer(destination);

            connection.Start();
            producer.DeliveryMode = MsgDeliveryMode.Persistent;
            request = session.CreateBytesMessage();
            request.NMSPriority = MsgPriority.Highest;
            request.NMSDeliveryMode = MsgDeliveryMode.Persistent;
        }

        public void Teardown() {
            if (session != null)
                session.Close();
            if (connection != null)
                connection.Close();
        }

        public void Receive() {
            IBytesMessage message = consumer.Receive() as IBytesMessage;
        }

        public void Send(byte[] message) {
            request.Content = message;
            producer.Send(request);
        }

    }
}
