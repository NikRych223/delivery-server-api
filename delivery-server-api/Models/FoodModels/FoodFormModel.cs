using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.FoodModels
{
    public class FoodFormModel
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Price { get; set; }
    }
}
