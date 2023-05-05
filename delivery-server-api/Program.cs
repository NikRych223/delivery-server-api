using delivery_server_api.Contexts;
using delivery_server_api.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "DevCors",
                                  policy =>
                                  {
                                      policy.AllowAnyHeader();
                                      policy.AllowAnyMethod();
                                      policy.AllowAnyOrigin();
                                  });
            });

            builder.Services.AddDbContext<FoodDBContext>(options
                => options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<FoodUserDbModel, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 12;
            }).AddEntityFrameworkStores<FoodDBContext>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();
            app.UseCors("DevCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}