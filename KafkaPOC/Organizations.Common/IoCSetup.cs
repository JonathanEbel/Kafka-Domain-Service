﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Organizations.Infrastructure;

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
            
            return services;
        }
    }
}
