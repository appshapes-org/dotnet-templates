using AppShapes.Core.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StartupBase = AppShapes.Core.Service.StartupBase;

namespace DotNetTemplate.Service
{
    public class Startup : StartupBase
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected virtual void ConfigureDatabase(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseNpgsql(connectionString);
        }

        protected virtual void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
        }

        // TODO: Replace DbContext with your domain context, e.g., InventoryContext.
        protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            base.ConfigureServices(services, configuration);
            new ConfigureDatabaseCommand().Execute<DbContext>(services, configuration, ConfigureEntityFramework, ConfigureDatabase);
        }
    }
}