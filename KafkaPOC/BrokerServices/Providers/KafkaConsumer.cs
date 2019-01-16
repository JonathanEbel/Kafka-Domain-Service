using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace BrokerServices.Providers
{
    public class KafkaConsumer : IMessageConsumer
    {
        private readonly MessageBrokerConfigSingleton _messageBrokerConfigSingleton;

        public KafkaConsumer(IOptions<MessageBrokerConfigSingleton> messageBrokerConfigSingleton)
        {
            _messageBrokerConfigSingleton = messageBrokerConfigSingleton.Value;
        }

        public bool ListenAndConsumeMessage<T>(string topic, Action<T> f)
        {
            var conf = new ConsumerConfig
            {
                GroupId = _messageBrokerConfigSingleton.GroupId,
                BootstrapServers = _messageBrokerConfigSingleton.BrokerLocation,

                AutoOffsetReset = AutoOffsetResetType.Earliest
            };

            using (var c = new Consumer<string, string>(conf))
            {
                c.Subscribe(topic);
                bool consuming = true;
                c.OnError += (_, e) => consuming = !e.IsFatal;

                while (consuming)
                {
                    try
                    {
                        var cr = c.Consume();
                        if (cr.Key.StartsWith(typeof(T).FullName))
                        {
                            f(JsonConvert.DeserializeObject<T>(cr.Value));
                        }
                    }
                    catch (ConsumeException e)
                    {
                        //LOG THIS error..
                    }
                }

                //close the consumer
                c.Close();
                return true;
            }
        }
    }
}
