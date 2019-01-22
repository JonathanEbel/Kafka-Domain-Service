using Core;
using System.Threading.Tasks;

namespace BrokerServices
{
    public interface IMessageProducer
    {
        Task<bool> ProduceEventAsync<T>(T ev) where T : EventBase;
        Task<bool> ProduceCommandAsync<T>(T cmd) where T : CommandBase;

    }
}
