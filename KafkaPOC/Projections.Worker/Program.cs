using Microsoft.Extensions.Configuration;
using System;

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
      //      var provider = IocSetup.SetUpContainer(configuration, (args[0].Trim() != "Release"));

        }
    }
}
