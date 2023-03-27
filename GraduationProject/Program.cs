using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using GraduationProject.Models;

namespace GraduationProject
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {

            //CreateHostBuilder(args)
            //   .Build().Run();

            var host = CreateHostBuilder(args)
                 .ConfigureAppConfiguration((hostContext, builder) =>
                 {
                     // Add other providers for JSON, etc.

                     if (hostContext.HostingEnvironment.IsDevelopment())
                     {
                         builder.AddUserSecrets<Program>();
                     }
                 })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var config = serviceProvider.GetRequiredService<IConfiguration>();

                await ApplicationDbContext.CreateDefaultAccounts(serviceProvider, config);
            }

            await host.RunAsync();



        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                   
                    webBuilder.UseStartup<Startup>();
                });
    }
}
