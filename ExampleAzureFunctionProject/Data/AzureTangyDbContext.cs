using ExampleAzureFunctionProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleAzureFunctionProject.Data
{
    public class AzureTangyDbContext : DbContext
    {
        public AzureTangyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SalesRequest> SalesRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesRequest>(entity =>
            {
                entity.HasKey(c => c.ID);
            });
        }
    }
}
