﻿using Confluent.Kafka;
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

        public async Task<bool> ProduceCommandAsync<T>(T cmd, string topicName) where T : CommandBase
        {
            string key = typeof(T).FullName + cmd.CommandId.ToString();
            string val = JsonConvert.SerializeObject(cmd);

            return await Produce(key, val, topicName);
        }


        public async Task<bool> ProduceEvent<T>(T ev, string topicName) where T : EventBase
        {
            string key = typeof(T).FullName + ev.CorrelationId.ToString();
            string val = JsonConvert.SerializeObject(ev);

            return await Produce(key, val, topicName);
        }


        private async Task<bool> Produce(string key, string val, string topicName)
        {
            try
            {
                var config = new ProducerConfig { BootstrapServers = _messageBrokerConfigSingleton.BrokerLocation };

                using (var producer = new Producer<string, string>(config))
                {
                    var deliveryReport = await producer.ProduceAsync(topicName, new Message<string, string> { Key = key, Value = val });
                }
            }
            catch (Exception ex)
            {
                //LOG THIS error and return false...
                return false;
            }

            return true;
        }

    }
}
