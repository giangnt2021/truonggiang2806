using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            
          .UseWindowsService()
          .ConfigureServices(configureServices);

        private static void configureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.Configure<ServiceConfig>(context.Configuration.GetSection("ServiceConfig"));
            services.AddLogging();
            services.AddSingleton<IServiceProcess, ServiceProcess>();
            services.AddHostedService<Worker>();
        }
    }
}
