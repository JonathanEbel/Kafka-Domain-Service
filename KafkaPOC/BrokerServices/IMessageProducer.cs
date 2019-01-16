using Core;
using System.Threading.Tasks;

namespace BrokerServices
{
    public interface IMessageProducer
    {
        Task<bool> ProduceEvent<T>(T ev, string brokerList, string topicName) where T : EventBase;
        Task<bool> ProduceCommandAsync<T>(T cmd, string brokerList, string topicName) where T : CommandBase;

    }
}
