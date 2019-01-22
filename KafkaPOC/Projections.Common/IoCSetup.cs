using BrokerServices;
using BrokerServices.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projections.Domain.EventHandlers;
using Projections.Domain.EventHandlers.Implementations;
using Projections.Domain.Repos;
using Projections.Infrastructure;
using Projections.Infrastructure.Repos;

namespace Projections.Common
{
    public class IoCSetup
    {
        public static ServiceProvider CustomSetup(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            var result = SetUpServicesObject(services, configuration);
            return result.BuildServiceProvider();
        }

        public static void CustomSetup(IServiceCollection services, IConfiguration configuration)
        {
            SetUpServicesObject(services, configuration);
        }

        private static IServiceCollection SetUpServicesObject(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<ProjectionsContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("ProjectionsConnection")));
            
            services.AddTransient<IUsageRepository, UsageRepository>();
            services.AddTransient<IHandleUserLoggedInEvent, HandleUserLoggedInEvent>();
            

            //current message broker
            services.Configure<MessageBrokerConfigSingleton>(configuration.GetSection("MessageBrokerSettings"));
            services.AddTransient<IMessageConsumer, KafkaConsumer>();

            return services;
        }
    }
}
