using System;
using AppShapes.Core;
using AppShapes.Core.Logging;
using AppShapes.Logging.Console;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetTemplate.Service
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().ConfigureLogging((_, x) => x.ClearProviders());
        }

        public static void Main(string[] args)
        {
            ((Program) ReflectionHelper.InvokeConstructor(ProgramType, null, null)).Run(args);
        }

        public virtual void Run(string[] args)
        {
            IWebHost host = GetWebHost(args);
            Logger.Information<Program>("web host running");
            host.Run();
            Logger.Information<Program>("web host exiting");
        }

        protected virtual void ConfigureLogging(WebHostBuilderContext context, ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
            logging.AddConsoleLogger();
            logging.AddDebug();
        }

        protected virtual void ConfigureSettings(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
        }

        protected virtual DbContext GetConfigurationContext(IServiceScope scope)
        {
            // TODO: Replace DbContext with your domain context, e.g., InventoryContext.
            return scope.ServiceProvider.GetRequiredService<DbContext>();
        }

        protected virtual IWebHost GetWebHost(string[] args)
        {
            IWebHost host = GetWebHostBuilder(args).Build();
            Setup(host);
            return host;
        }

        protected virtual IWebHostBuilder GetWebHostBuilder(string[] args)
        {
            IWebHostBuilder builder = CreateWebHostBuilder(args);
            builder.ConfigureLogging(ConfigureLogging);
            builder.ConfigureAppConfiguration(ConfigureSettings);
            return builder;
        }

        protected virtual void MigrateDatabase(IWebHost host)
        {
            Logger.LogInformation($"[{nameof(MigrateDatabase)}] called");
            using (IServiceScope scope = host.Services.CreateScope())
            {
                DbContext context = GetConfigurationContext(scope);
                context.Database.Migrate();
            }

            Logger.LogInformation($"[{nameof(MigrateDatabase)}] exiting");
        }

        protected static Type ProgramType { get; set; } = typeof(Program);

        protected virtual void Setup(IWebHost host)
        {
            Logger = host.Services.GetRequiredService<ILogger<Program>>();
            if (ShouldMigrateDatabase(host.Services.GetRequiredService<IConfiguration>()))
                MigrateDatabase(host);
        }

        protected virtual bool ShouldMigrateDatabase(IConfiguration configuration)
        {
            bool shouldMigrateDatabase = Convert.ToBoolean(configuration["AppSettings:AutoMigrateDatabase"]);
            Logger.LogDebug($"[{nameof(ShouldMigrateDatabase)}] {shouldMigrateDatabase}");
            return shouldMigrateDatabase;
        }

        private ILogger<Program> Logger { get; set; }
    }
}