using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.FoodModels
{
    public class FoodDbModel
    {
        public Guid FoodId { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(20)]
        public string Price { get; set; }

        [Required]
        public virtual Image Image { get; set; }
    }
}
