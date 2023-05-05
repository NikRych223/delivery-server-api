using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.Favorite
{
    public class FavoriteAddModel
    {
        [Required]
        public Guid FoodId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
    }
}
