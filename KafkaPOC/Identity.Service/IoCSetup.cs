using Identity.Domain.CommandHandlers;
using Identity.Domain.Repos;
using Identity.Infrastructure;
using Identity.Infrastructure.Repos;
using Identity.Service.CommandHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Service
{
    public static class IoCSetup
    {
        public static void CustomSetup(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql().AddDbContext<IdentityContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));

            services.Configure<AppSettingsSingleton>(configuration.GetSection("AppSettings"));

            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IPasswordResetCommandHandler, PasswordResetCommandHandler>();
            services.AddTransient<ICreateNewApplicationUserCommandHandler, CreateNewApplicationUserCommandHandler>();
        }
    }
}
