using Microsoft.EntityFrameworkCore;
using mile3image.Entities;

namespace mile3image.Database
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

    }
}
