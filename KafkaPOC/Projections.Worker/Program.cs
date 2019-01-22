using BrokerServices;
using Microsoft.Extensions.Configuration;
using Projections.Common;
using System;
using Microsoft.Extensions.DependencyInjection;
using Identity.Events;
using Microsoft.Extensions.Options;
using Projections.Domain.EventHandlers;

namespace Projections.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //one argument must be passed in which is the environment to run as..
            Console.WriteLine("Running as environment: " + args[0].Trim());

            //set up configuration...
            IConfigurationRoot configuration = ConfigurationSetup.SetUpConfigs(args[0].Trim());

            //set up my IoC container....
            var provider = IoCSetup.CustomSetup(configuration);

            //resolve my dependencies
            var consumer = provider.GetService<IMessageConsumer>();
            var brokerSetings = provider.GetService<IOptions<MessageBrokerConfigSingleton>>().Value;
            var userLoggedInEventHandler = provider.GetService<IHandleUserLoggedInEvent>();

            consumer.ListenAndConsumeMessage<UserLoggedInEvent>(brokerSetings.EventsTopicName, userLoggedInEventHandler.Handle);
        }
    }
}
