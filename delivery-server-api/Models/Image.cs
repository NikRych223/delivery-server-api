using delivery_server_api.Models.FoodModels;
using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string LocalPath { get; set; }

        public Guid FoodItemId { get; set; }

        [Required]
        public FoodDbModel FoodItem { get; set; }
    }
}
