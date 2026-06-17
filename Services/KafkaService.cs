using Confluent.Kafka;
using Microsoft.Extensions.Options;
using ChineseAuctionAPI.Interface;

namespace ChineseAuctionAPI.Services
{
    
    public class KafkaProducerService : IKafkaProducerService, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducerService> _logger;

        public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
        {
            _logger = logger;

            // ProducerConfig מקבל את פרטי החיבור מה-appsettings
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"],
                MessageSendMaxRetries = 3,
                RetryBackoffMs = 1000,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(string topic, string key, string value)
        {
            var message = new Message<string, string> { Key = key, Value = value };

            var result = await _producer.ProduceAsync(topic, message);

            _logger.LogInformation(
                "Message sent to topic={Topic}, partition={Partition}, offset={Offset}",
                result.Topic, result.Partition, result.Offset);
        }

        public void Dispose() => _producer?.Dispose();
    }
}