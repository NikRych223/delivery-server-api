using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.FoodModels
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Price { get; set; }
    }
}
