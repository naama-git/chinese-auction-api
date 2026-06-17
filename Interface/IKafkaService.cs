
namespace ChineseAuctionAPI.Interface
{
    public interface IKafkaProducerService
    {
        public Task ProduceAsync(string topic, string key, string value);
    }
}