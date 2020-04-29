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

        protected override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            base.ConfigureServices(services, configuration);
            // TODO: Replace DbContext with your domain context, e.g., InventoryContext.
            services.AddEntityFrameworkNpgsql().AddDbContext<DbContext>(UsePostgreSQL);
        }

        protected virtual string GetConnectionString()
        {
            return Configuration.GetConnectionString("DatabaseConnection");
        }

        protected virtual void UsePostgreSQL(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(GetConnectionString());
        }
    }
}