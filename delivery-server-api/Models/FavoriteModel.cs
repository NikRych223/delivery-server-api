using delivery_server_api.Models.ApplicationUser;

namespace delivery_server_api.Models
{
    public class FavoriteModel
    {
        public Guid Id { get; set; }
        public Guid FoodId { get; set; }

        public string UserId { get; set; }
        public FoodUserDbModel User { get; set; }
    }
}
