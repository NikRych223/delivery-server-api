using delivery_server_api.Models;
using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.FoodModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Contexts
{
    public class FoodDBContext : IdentityDbContext
    {
        public required DbSet<FoodDbModel> FoodItems { get; set; }

        public FoodDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // FOOD DB CONTEXT CONFIG

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
                .HasForeignKey<Image>(i => i.FoodId)
                .IsRequired();
            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);

            // USER DB CONTEXT CONFIG

            modelBuilder.Entity<FoodUserDbModel>()
                .Property(x => x.UserName)
                .IsRequired();
            modelBuilder.Entity<FoodUserDbModel>()
                .Property(x => x.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<FoodUserDbModel>()
                .Property(x => x.Email)
                .IsRequired();
            modelBuilder.Entity<FoodUserDbModel>()
                .Property (x => x.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<FoodUserDbModel>()
                .HasMany(x => x.Favorite)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FavoriteModel>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<FavoriteModel>()
                .Property(x => x.FoodId)
                .IsRequired();
        }

    }
}