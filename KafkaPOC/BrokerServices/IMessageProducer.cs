using Core;
using System.Threading.Tasks;

namespace BrokerServices
{
    public interface IMessageProducer
    {
        Task<bool> ProduceEvent<T>(T ev, string topicName) where T : EventBase;
        Task<bool> ProduceCommandAsync<T>(T cmd, string topicName) where T : CommandBase;

    }
}
