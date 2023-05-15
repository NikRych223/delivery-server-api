using delivery_server_api.Models.Cart;
using delivery_server_api.Models.Favorite;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.ApplicationUser
{
    public class UserDbModel : IdentityUser
    {
        [StringLength(100)]
        public string Addres { get; set; }

        public virtual List<FavoriteDbModel> Favorites { get; } = new List<FavoriteDbModel>();
        public virtual List<CartDbModel> Carts { get; } = new List<CartDbModel>();
    }
}
