using AspNet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> entities { get; set; }
    }
}
