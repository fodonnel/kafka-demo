using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaDemoApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "produce")
            {
                await Produce();
            }
            else
            {
                await Consume();
            }
        }

        static async Task Produce()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<string, string>(config).Build())
            {
                for(int i=0; i< 100; i++)
                {
                    var key = "key1";
                    var message = Guid.NewGuid().ToString();

                    var dr = await p.ProduceAsync("demo-topic-1", new Message<string, string>
                    {
                        Key = key,
                        Value = message
                    });
                }   
            }
        }


        static async Task Consume()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "demo-consumer-group",
                BootstrapServers = "localhost:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("demo-topic-1");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {

                        var cr = c.Consume(cts.Token);
                        Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
           
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Commit();
                    c.Close();
                }
            }
        }
    }
}
