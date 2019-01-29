using Confluent.Kafka;
using Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BrokerServices.Providers
{
    public class KafkaProducer : IMessageProducer
    {
        private readonly MessageBrokerConfigSingleton _messageBrokerConfigSingleton;

        public KafkaProducer(IOptions<MessageBrokerConfigSingleton> messageBrokerConfigSingleton)
        {
            _messageBrokerConfigSingleton = messageBrokerConfigSingleton.Value;
        }

        public async Task<bool> ProduceCommandAsync<T>(T cmd) where T : CommandBase
        {
            string key = typeof(T).FullName;
            string val = JsonConvert.SerializeObject(cmd);

            return await Produce(key, val, _messageBrokerConfigSingleton.CommandsTopicName);
        }


        public async Task<bool> ProduceEventAsync<T>(T ev) where T : EventBase
        {
            string key = typeof(T).FullName + ev.EntityId.ToString();
            string val = JsonConvert.SerializeObject(ev);

            return await Produce(key, val, _messageBrokerConfigSingleton.EventsTopicName);
        }


        private async Task<bool> Produce(string key, string val, string topicName)
        {
            try
            {
                var config = new ProducerConfig { BootstrapServers = _messageBrokerConfigSingleton.BrokerLocation, MessageTimeoutMs = 5000, MessageSendMaxRetries = 3 };

                using (var producer = new Producer<string, string>(config))
                {
                    var deliveryReport = await producer.ProduceAsync(topicName, new Message<string, string> { Key = key, Value = val });
                }
            }
            catch (Exception ex)
            {
                if (ex is KafkaException)
                {
                    //log that we failed to deliver message...
                }

                //LOG THIS error and return false...
                return false;
            }
            
            return true;
        }

    }
}
