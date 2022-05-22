using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sicpa.Api.Application.Persistence;
using System;

namespace SicpaServices.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateHostBuilder(args).Build();

            using (var context = hostserver.Services.CreateScope())
            {
                var services = context.ServiceProvider;

                try
                {
                    var db = services.GetRequiredService<SicDbContext>();
                    var logger = services.GetRequiredService<ILogger<SecurityData>>();

                    SecurityData.InsertAdminUser(db, logger).Wait();
                }
                catch (Exception ex)
                {
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(ex, "Error en creación de usuario admin");
                }
            }

            hostserver.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
