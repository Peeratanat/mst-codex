using External.Kafka.Models;

namespace External.Kafka.Interface
{
    public interface IKakfaProducer
    {
        Task ProduceMessage(string topic, object data);
        Task WriteApplicationLog(ApplicationLog log); 
    }
}
