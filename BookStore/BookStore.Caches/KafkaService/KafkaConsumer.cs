using BookStore.Caches.KafkaService.GenericSerAndDeser;
using BookStore.Models.Models.Configurations;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace BookStore.Caches.KafkaService
{
    public class KafkaConsumer<TKey, TValue>
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly IOptions<KafkaConfiguration> _kafkaSettings;
        private readonly List<TValue> _memoList;
        public KafkaConsumer(IOptions<KafkaConfiguration> myJsonSettings)
        {
            _kafkaSettings = myJsonSettings;
            _consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = _kafkaSettings.Value.BootstrapServers,
                AutoOffsetReset = (AutoOffsetReset)_kafkaSettings.Value.AutoOffsetReset,
                GroupId = _kafkaSettings.Value.GroupId,
            };
            _memoList = new List<TValue>();
        }

        public Task Consume(CancellationToken cancellationToken)
        {
            var consumer = new ConsumerBuilder<TKey, TValue>(_consumerConfig).SetValueDeserializer(new DeserializeGen<TValue>())
                                                                             .SetKeyDeserializer(new DeserializeGen<TKey>()).Build();
            consumer.Subscribe(typeof(TValue).Name);
            Task.Run(() =>
            {

                while (true)
                {
                    var result = consumer.Consume();
                    if (result != null)
                    {
                        _memoList.Add(result.Value);
                    }
                }
            },cancellationToken);
            return Task.CompletedTask;
        }
        public List<TValue> ReturnValues()
        {
            return _memoList;
        }

    }
}
