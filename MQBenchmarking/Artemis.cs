﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQBenchmarking {
    class Artemis : MessageQueue {
        int numMessages = 0, messageSize = 0;

        public void Setup(int numMessages, int messageSize) {
            throw new NotImplementedException();
        }

        public void Teardown() {

        }

        public void Receive() {
            throw new NotImplementedException();
        }

        public void Send(byte[] message) {
            throw new NotImplementedException();
        }

    }
}