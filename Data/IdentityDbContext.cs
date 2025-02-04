using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class IdentityDbContext
    {
        private DbContextOptions options;

        public IdentityDbContext(DbContextOptions options)
        {
            this.options = options;
        }

      
        internal void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}