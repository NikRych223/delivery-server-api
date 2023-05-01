namespace delivery_server_api.Models
{
    public class FoodDbModel
    {
        public Guid FoodId { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }

        public virtual Image Image { get; set; }
    }
}
