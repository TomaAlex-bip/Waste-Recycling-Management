using Microsoft.EntityFrameworkCore;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Data.EntityConfigurations;

namespace WasteRecyclingManagementApi.Data
{
    public class RecyclingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RecyclingPoint> RecyclingPoints { get; set; }
        public DbSet<Container> Containers { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public RecyclingDbContext(DbContextOptions<RecyclingDbContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Container>(new ContainerConfiguration());
            modelBuilder.ApplyConfiguration<RecyclingPoint>(new RecyclingPointConfiguration());
            modelBuilder.ApplyConfiguration<Operation>(new OperationConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
