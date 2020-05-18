using System;
using AppShapes.Core;
using AppShapes.Core.Database;
using AppShapes.Core.Service;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotNetTemplate.Service
{
    public class Program : ProgramBase
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            return builder.UseStartup<Startup>().ConfigureLogging((_, x) => x.ClearProviders());
        }

        public static void Main(string[] args)
        {
            ((Program) ReflectionHelper.InvokeConstructor(ProgramType, null, null)).Run(args);
        }

        protected override IWebHostBuilder GetWebHostBuilder(string[] args)
        {
            return CreateWebHostBuilder(args);
        }

        protected override void Initialize(IServiceProvider provider)
        {
            base.Initialize(provider);
            InitializeDatabase(provider);
        }

        protected virtual void InitializeDatabase(IServiceProvider provider)
        {
            ILogger<InitializeDatabaseCommand> logger = provider.GetRequiredService<ILogger<InitializeDatabaseCommand>>();
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
            new InitializeDatabaseCommand(logger, configuration, provider.CreateScope).Execute();
        }

        protected static Type ProgramType { get; set; } = typeof(Program);
    }
}