using System;

namespace BrokerServices
{
    public interface IMessageConsumer
    {
        bool ListenAndConsumeMessage<T>(string groupId, string broker, string topic, Action<T> f);
    }
}
