using BrokerServices;
using BrokerServices.Providers;
using Identity.Domain.CommandHandlers;
using Identity.Domain.CommandHandlers.Implementations;
using Identity.Domain.Repos;
using Identity.Infrastructure;
using Identity.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Common
{
    public static class IoCSetup
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
            services.AddEntityFrameworkNpgsql().AddDbContext<IdentityContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));

            services.Configure<AppSettingsSingleton>(configuration.GetSection("AppSettings"));

            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IPasswordResetCommandHandler, PasswordResetCommandHandler>();
            services.AddTransient<ICreateNewApplicationUserCommandHandler, CreateNewApplicationUserCommandHandler>();
            services.AddTransient<ILoginCommandHandler, LoginCommandHandler>();

            //current message broker
            services.Configure<MessageBrokerConfigSingleton>(configuration.GetSection("MessageBrokerSettings"));
            services.AddTransient<IMessageProducer, KafkaProducer>();

            return services;
        }
    }
}
