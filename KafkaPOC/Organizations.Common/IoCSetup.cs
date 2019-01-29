using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Domain.CommandHandlers;
using Organizations.Domain.CommandHandlers.Implementations;
using Organizations.Domain.Repos;
using Organizations.Infrastructure;
using Organizations.Infrastructure.Repos;

namespace Organizations.Common
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
            services.AddEntityFrameworkNpgsql().AddDbContext<OrganizationsContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("OrganizationsConnection")));

            services.AddTransient<IStateProvinceRepository, StateProvinceRepository>();
            services.AddTransient<IOrgTypeRepository, OrgTypeRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IAddOrganizationCommandHandler, AddOrganizationCommandHandler>();
            services.AddTransient<ICreateNewUserCommandHandler, CreateNewUserCommandHandler>();

            return services;
        }
    }
}
