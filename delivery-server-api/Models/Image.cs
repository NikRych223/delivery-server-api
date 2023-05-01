namespace delivery_server_api.Models
{
    public class Image
    {
        public Guid ImageId { get; set; }
        public string ImageName { get; set; }
        public string LocalPath { get; set; }

        public Guid FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
