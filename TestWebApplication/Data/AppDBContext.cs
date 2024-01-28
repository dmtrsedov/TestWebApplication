using Microsoft.EntityFrameworkCore;

namespace TestWebApplication.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        
    }
}
