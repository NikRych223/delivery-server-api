using delivery_server_api.Models.Favorite;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.ApplicationUser
{
    public class FoodUserDbModel : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Addres { get; set; }

        public virtual List<FavoriteItem> Favorites { get; } = new List<FavoriteItem>();
    }
}
