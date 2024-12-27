using SportStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace SportStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; } 
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Categories>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories) 
                .WithMany(c => c.Products) 
                .HasForeignKey(p => p.CategoryId);

            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().HasData(
                new Categories { Id = 1, Name = "Voetbal" },
                new Categories { Id = 2, Name = "Tennis" },
                new Categories { Id = 3, Name = "Wielrennen" },
                new Categories { Id = 4, Name = "Basketbal" },
                new Categories { Id = 5, Name = "Zwemmen" },
                new Categories { Id = 6, Name = "Hardlopen" },
                new Categories { Id = 7, Name = "Honkbal" },
                new Categories { Id = 8, Name = "Golf" },
                new Categories { Id = 9, Name = "Skiën" },
                new Categories { Id = 10, Name = "Schaatsen" }
            );
        }
    }
}
