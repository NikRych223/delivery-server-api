using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.ApplicationUser
{
    public class FoodUserDbModel : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Addres { get; set; }

        public List<FavoriteModel>? Favorite { get; set; }
    }
}
