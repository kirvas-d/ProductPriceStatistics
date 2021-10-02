using Microsoft.EntityFrameworkCore;
using ProductPriceStatistics.Infrastructure.EFCoreRepository.Models;

namespace ProductPriceStatistics.Infrastructure.EFCoreRepository
{
    public class ProductPriceStatisticsDbContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public ProductPriceStatisticsDbContext()
            : base()
        {
        }

        public ProductPriceStatisticsDbContext(DbContextOptions<ProductPriceStatisticsDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasIndex(e => e.ProductId, "IX_Prices_ProductId");

                entity.HasIndex(e => e.StoreId, "IX_Prices_StoreId");

            });

            modelBuilder.Entity<Price>()
                .HasOne(d => d.Product)
                .WithMany(p => p.Prices)
                .HasForeignKey(d => d.ProductId);

            modelBuilder.Entity<Price>()
                .HasOne(d => d.Store)
                .WithMany(p => p.Prices)
                .HasForeignKey(d => d.StoreId);

            modelBuilder
                .Entity<Product>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Products)
                .UsingEntity(j => j.ToTable("ProductTags"));

        }
    }
}
