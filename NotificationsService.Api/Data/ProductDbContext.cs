using Microsoft.EntityFrameworkCore;
using NotificationsService.Api.Models;

namespace NotificationsService.Api.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(product => product.Id);
                entity.Property(product => product.Name).HasMaxLength(120).IsRequired();
                entity.Property(product => product.Description).HasMaxLength(500);
                entity.Property(product => product.Price).HasPrecision(10, 2);
                entity.Property(product => product.CreatedAt).IsRequired();
            });
        }
    }
}
