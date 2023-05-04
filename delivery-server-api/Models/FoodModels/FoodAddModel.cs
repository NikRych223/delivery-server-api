using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.FoodModels
{
    public class FoodAddModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Price { get; set; }
    }
}
