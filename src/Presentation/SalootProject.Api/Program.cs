using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SalootProject.Api.Configuration;
using Serilog;

namespace SalootProject.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot configuration =
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            SerilogConfiguration.Register(configuration);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                          .UseStartup<Startup>()
                          .UseSerilog()
                          .Build();
        }
    }
}