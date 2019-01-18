using Microsoft.Extensions.Configuration;
using System.IO;

namespace Projections.Worker
{
    public class ConfigurationSetup
    {
        public static IConfigurationRoot SetUpConfigs(string environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings." + environment + ".json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
