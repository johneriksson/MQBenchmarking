using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.Util;
using Apache.NMS.ActiveMQ;

namespace MQBenchmarking {
    class ActiveMQ : MessageQueue {
        int numMessages = 0, messageSize = 0;
        Uri connectUri = new Uri("activemq:tcp://localhost:61616");
        IConnectionFactory factory = null;
        IConnection connection = null;
        ISession session = null;
        IDestination destination = null;
        IMessageConsumer consumer = null;
        IMessageProducer producer = null;
        IBytesMessage request = null;

        public void Setup(int numMessages, int messageSize) {
            this.numMessages = numMessages;
            this.messageSize = messageSize;

            factory = new NMSConnectionFactory(connectUri);
            connection = factory.CreateConnection();
            session = connection.CreateSession();

            destination = SessionUtil.GetDestination(session, "queue://FOO.BAR");
            consumer = session.CreateConsumer(destination);
            producer = session.CreateProducer(destination);

            connection.Start();
            producer.DeliveryMode = MsgDeliveryMode.Persistent;
            request = session.CreateBytesMessage();
            request.NMSDeliveryMode = MsgDeliveryMode.Persistent;
            request.NMSCorrelationID = "abc";
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
            producer.Send(request);
        }

    }
}
