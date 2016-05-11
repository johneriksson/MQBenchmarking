namespace MQBenchmarking {
    interface MessageQueue {
        void Setup(int numMessages, int messageSize);
        void Teardown();
        void Send(byte[] message);
        void Receive();
    }
}
