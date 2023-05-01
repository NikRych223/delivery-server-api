using delivery_server_api.Models;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Contexts
{
    public class FoodDBContext : DbContext
    {
        public required DbSet<FoodItem> FoodItems { get; set; }

        public FoodDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodItem>()
                .HasKey(fi => fi.FoodId);
            modelBuilder.Entity<FoodItem>()
                .Property(fi => fi.Title)
                .IsRequired();
            modelBuilder.Entity<FoodItem>()
                .HasIndex(fi => fi.Title)
                .IsUnique();
            modelBuilder.Entity<FoodItem>()
                .Property(fi => fi.Price)
                .IsRequired();
            modelBuilder.Entity<FoodItem>()
                .HasOne(f => f.Image)
                .WithOne(f => f.FoodItem)
                .HasForeignKey<Image>(i => i.FoodItemId)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);
        }

    }
}