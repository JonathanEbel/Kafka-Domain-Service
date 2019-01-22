namespace BrokerServices
{
    public class MessageBrokerConfigSingleton
    {
        public string GroupId { get; set; }
        public string BrokerLocation { get; set; }
        public string CommandsTopicName { get; set; }
        public string EventsTopicName { get; set; }
    }
}
