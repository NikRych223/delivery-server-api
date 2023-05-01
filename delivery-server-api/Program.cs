using delivery_server_api.Contexts;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FoodDBContext>(options 
                => options.UseSqlServer(connectionString));

            builder.Services.AddControllers();

            var app = builder.Build();
            
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}