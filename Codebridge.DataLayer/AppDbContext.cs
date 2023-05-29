using Codebridge.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Codebridge.DataLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Dog> Dogs { get; set; }
    }
}
