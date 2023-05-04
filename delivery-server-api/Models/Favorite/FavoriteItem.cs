using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.FoodModels;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.Favorite
{
    public class FavoriteItem
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [Required]
        public virtual FoodUserDbModel User { get; set; }

        public Guid FoodId { get; set; }
        public virtual FoodDbModel Food { get; set; }
    }
}
