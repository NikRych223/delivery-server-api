using delivery_server_api.Models;
using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.Favorite;
using delivery_server_api.Models.FoodModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Contexts
{
    public class FoodDBContext : IdentityDbContext
    {
        public required DbSet<FoodDbModel> FoodItems { get; set; }
        public DbSet<FavoriteDbModel> Favorite { get; set; }

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
            modelBuilder.Entity<FoodDbModel>()
                .HasMany(x => x.Favorites)
                .WithOne(x => x.Food)
                .HasForeignKey(x => x.FoodId);
            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId);

            // USER DB CONTEXT CONFIG

            modelBuilder.Entity<UserDbModel>()
                .Property(x => x.UserName)
                .IsRequired();
            modelBuilder.Entity<UserDbModel>()
                .Property(x => x.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<UserDbModel>()
                .Property(x => x.Email)
                .IsRequired();
            modelBuilder.Entity<UserDbModel>()
                .Property (x => x.PhoneNumber)
                .IsRequired();
            modelBuilder.Entity<UserDbModel>()
                .Property(x => x.Addres)
                .IsRequired();
            modelBuilder.Entity<UserDbModel>()
                .HasMany(x => x.Favorites)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FavoriteDbModel>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<FavoriteDbModel>()
                .Property(x => x.FoodId)
                .IsRequired();
        }

    }
}