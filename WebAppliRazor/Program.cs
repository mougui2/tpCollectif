using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionMagasin;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAppliRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var builder = CreateWebHostBuilder(args).Build();

            var dbContext = (TpDbContext)builder.Services.CreateScope().ServiceProvider.GetRequiredService(typeof(TpDbContext));
            dbContext.Database.EnsureCreated();
            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
