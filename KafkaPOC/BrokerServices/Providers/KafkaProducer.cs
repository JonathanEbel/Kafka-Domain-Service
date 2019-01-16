using Confluent.Kafka;
using Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BrokerServices.Providers
{
    class KafkaProducer : IMessageProducer
    {
        public async Task<bool> ProduceCommandAsync<T>(T cmd, string brokerList, string topicName) where T : CommandBase
        {
            string key = typeof(T).FullName + cmd.CommandId.ToString();
            string val = JsonConvert.SerializeObject(cmd);

            return await Produce(key, val, brokerList, topicName);
        }


        public async Task<bool> ProduceEvent<T>(T ev, string brokerList, string topicName) where T : EventBase
        {
            string key = typeof(T).FullName + ev.CorrelationId.ToString();
            string val = JsonConvert.SerializeObject(ev);

            return await Produce(key, val, brokerList, topicName);
        }


        private async Task<bool> Produce(string key, string val, string brokerList, string topicName)
        {
            try
            {
                var config = new ProducerConfig { BootstrapServers = brokerList };

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
