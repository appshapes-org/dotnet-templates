using Microsoft.EntityFrameworkCore;

namespace DotNetTemplate.Infrastructure
{
    // TODO: Rename this to match your domain (e.g., PersonContext).
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}