using Confluent.Kafka;
using Newtonsoft.Json;
using System;

namespace BrokerServices.Providers
{
    public class KafkaConsumer : IMessageConsumer
    {
        public bool ListenAndConsumeMessage<T>(string groupId, string broker, string topic, Action<T> f)
        {
            var conf = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = broker,

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
