namespace delivery_server_api.Models
{
    public class FoodFormModel
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
    }
}
