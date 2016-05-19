using System;

namespace MQBenchmarking {
    class Tester {
        public static void Test(string messageQueue, string option, int numMessages, int messageSize) {
            byte[] message = new byte[messageSize];
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

            Console.Write("Setting up message queue... ");
            mq.Setup();
            System.Threading.Thread.Sleep(2000);
            Console.Write("Done.\n");

            Console.Write("Warming up... ");
            for(int i = 0; i < 1000; i++) {
                mq.Send(message);
            }
            for(int i = 0; i < 1000; i++) {
                mq.Receive();
            }
            Console.Write("Done.\n");

            Console.Write("Running garbage collector... ");
            GC.Collect();
            Console.Write("Done.\n");

            Console.Write("Running tests... ");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            if (option == "send") {
                for (int i = 0; i < numMessages; i++) {
                    //Console.WriteLine(i);
                    mq.Send(message);
                }
            } else if (option == "receive") {
                for (int i = 0; i < numMessages; i++) {
                    //Console.WriteLine(i);
                    mq.Receive();
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            var elapsedS = (float) watch.ElapsedMilliseconds / 1000;
            var msgPerS = Math.Round(numMessages / elapsedS);
            string action = option == "send" ? "Sent" : "Received";
            Console.Write("Done.\n");

            Console.Write("Finished! " + action + " " + numMessages + " messages in " + elapsedMs + " ms. (" + msgPerS + " msg/s). \nRunning teardown()... ");
            System.Threading.Thread.Sleep(1000);
            mq.Teardown();
            Console.Write("Done.\n");
        }
    }
}
