using System;

namespace BrokerServices
{
    public interface IMessageConsumer
    {
        bool ListenAndConsumeMessage<T>(string topic, Action<T> f);
    }
}
