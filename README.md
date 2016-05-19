# MQBenchmarking
C# project that attempts to benchmark RabbitMQ, Apache ActiveMQ and Apache Artemis

## How to run
1. Download and build
2. Open cmd or PowerShell and cd to bin/Debug
3. run `./MQBenchmarking <broker_name> <action> <num_messages> <message_size>`

example: <br>
`./MQBenchmarking rabbitmq send 10000 4096` <br>
to send 10000 messages with a size of 4096 bytes using RabbitMQ
