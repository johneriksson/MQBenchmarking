namespace MQBenchmarking {
    interface MessageQueue {
        void Setup();
        void Teardown();
        void Send(byte[] message);
        void Receive();
    }
}
