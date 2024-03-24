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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasMany(x => x.positions).WithOne();
            modelBuilder.Entity<Department>().HasMany(x => x.departments).WithOne().HasForeignKey(x => x.departmentId);
            modelBuilder.Entity<Jobers>().HasOne(x => x.position).WithOne();
            modelBuilder.Entity<Jobers>().HasOne(x => x.department).WithOne();
        }
    }
}
