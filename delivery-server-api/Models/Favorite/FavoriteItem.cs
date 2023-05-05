using delivery_server_api.Models.ApplicationUser;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.Favorite
{
    public class FavoriteItem
    {
        public Guid Id { get; set; }
        [Required]
        public Guid FoodId { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public FoodUserDbModel User { get; set; }
    }
}
