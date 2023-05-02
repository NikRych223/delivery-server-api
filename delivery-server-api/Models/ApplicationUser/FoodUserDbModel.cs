using Microsoft.AspNetCore.Identity;

namespace delivery_server_api.Models.ApplicationUser
{
    public class FoodUserDbModel : IdentityUser
    {
        public List<FavoriteModel>? Favorite { get; set; }
    }
}
