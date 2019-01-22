using BrokerServices;
using BrokerServices.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddEntityFrameworkNpgsql().AddDbContext<ProjectionsContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("ProjectionsConnection")));
            
            services.AddTransient<IUsageRepository, UsageRepository>();
            

            //current message broker
            services.Configure<MessageBrokerConfigSingleton>(configuration.GetSection("MessageBrokerSettings"));
            services.AddTransient<IMessageProducer, KafkaProducer>();

            return services.BuildServiceProvider();
        }
    }
}
