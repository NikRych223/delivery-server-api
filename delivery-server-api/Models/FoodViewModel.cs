using Microsoft.AspNetCore.Mvc;

namespace delivery_server_api.Models
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public FileContentResult Image { get; set; }
    }
}
