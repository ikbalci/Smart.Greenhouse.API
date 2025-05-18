using Microsoft.EntityFrameworkCore;
using Smart.Greenhouse.API.Core.Entities;

namespace Smart.Greenhouse.API.Infrastructure.Data
{
    public class SensorDataContext : DbContext
    {
        public SensorDataContext(DbContextOptions<SensorDataContext> options) : base(options)
        {
        }

        public DbSet<SensorData> SensorData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SensorData>()
                .HasIndex(s => s.CreatedAt);
        }
    }
} 