using CICWebApi.Entities;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedRequests)
                .WithOne(r => r.CreatorUser)
                .HasForeignKey(r => r.CreatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ExecutedRequests)
                .WithOne(r => r.ExecutorUser)
                .HasForeignKey(r => r.ExecutorUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
