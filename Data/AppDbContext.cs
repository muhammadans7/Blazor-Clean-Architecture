using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Data
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }
    }

} 



