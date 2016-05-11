using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQBenchmarking {
    interface MessageQueue {
        void Setup(int numMessages, int messageSize);
        void Teardown();
        void Send(byte[] message);
        void Receive();
    }
}
