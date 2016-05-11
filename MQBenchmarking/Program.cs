using System;
using System.Linq;

namespace MQBenchmarking {
    class Program {
        static string[] MessageQueues = new string[] { "rabbitmq", "activemq", "artemis" };
        static string[] Option = new string[] { "send", "receive" };

        static void Main(string[] args) {
            int numMessages = 0, messageSize = 0;

            if (args.Length != 4 ||
                !MessageQueues.Contains(args[0].ToLower()) ||
                !Option.Contains(args[1].ToLower()) ||
                !Int32.TryParse(args[2], out numMessages) ||
                !Int32.TryParse(args[3], out messageSize)) {

                Console.WriteLine("usage: MQBenchmarking {rabbitmq, activemq, artemis} {send, receive} <num_messages> <message_size>");
                Environment.Exit(0);
            }

            string messageQueue = args[0].ToLower();
            string option = args[1].ToLower();

            Tester.Test(messageQueue, option, numMessages, messageSize);
            Environment.Exit(0);
        }
    }
}
