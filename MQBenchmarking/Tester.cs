using System;

namespace MQBenchmarking {
    class Tester {
        public static void Test(string messageQueue, string option, int numMessages, int messageSize) {
            MessageQueue mq = null;
            switch (messageQueue) {
                case "rabbitmq":
                    mq = new RabbitMQ();
                    break;

                case "activemq":
                    mq = new ActiveMQ();
                    break;

                case "artemis":
                    mq = new Artemis();
                    break;
            }

            Console.WriteLine("Setting up message queue...");
            mq.Setup();

            Console.WriteLine("Running tests...");
            if (option == "send") {
                byte[] message = new byte[messageSize];
                for (int i = 0; i < numMessages; i++) {
                    mq.Send(message);
                }
            } else if (option == "receive") {
                for (int i = 0; i < numMessages; i++) {
                    mq.Receive();
                }
            }

            Console.WriteLine("Finished tests! Running teardown()");
            System.Threading.Thread.Sleep(2000);
            mq.Teardown();
        }
    }
}
