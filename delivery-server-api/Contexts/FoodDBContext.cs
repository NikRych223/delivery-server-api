using delivery_server_api.Models;
using delivery_server_api.Models.FoodModels;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Contexts
{
    public class FoodDBContext : DbContext
    {
        public required DbSet<FoodDbModel> FoodItems { get; set; }

        public FoodDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FoodDbModel>()
                .HasKey(fi => fi.FoodId);
            modelBuilder.Entity<FoodDbModel>()
                .Property(fi => fi.Title)
                .IsRequired();
            modelBuilder.Entity<FoodDbModel>()
                .HasIndex(fi => fi.Title)
                .IsUnique();
            modelBuilder.Entity<FoodDbModel>()
                .Property(fi => fi.Price)
                .IsRequired();
            modelBuilder.Entity<FoodDbModel>()
                .HasOne(f => f.Image)
                .WithOne(f => f.FoodItem)
                .HasForeignKey<Image>(i => i.FoodItemId)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);
        }

    }
}