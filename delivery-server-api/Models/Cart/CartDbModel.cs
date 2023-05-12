using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.FoodModels;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.Cart
{
    public class CartDbModel
    {
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual UserDbModel User { get; set; }

        [Required]
        public Guid FoodId { get; set; }

        [Required]
        public virtual FoodDbModel Food { get; set; }
    }
}
