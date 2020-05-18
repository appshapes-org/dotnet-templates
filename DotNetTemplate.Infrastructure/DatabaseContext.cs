using AppShapes.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure
{
    // TODO: Rename this to match your domain (e.g., PersonContext).
    public class DatabaseContext : OutboxContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}