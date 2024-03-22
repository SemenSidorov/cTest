using cTest.Models;
using Microsoft.EntityFrameworkCore;

namespace cTest
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) { }

        public DbSet<Jobers> Jobers { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Positions> Positions { get; set; }
    }
}
