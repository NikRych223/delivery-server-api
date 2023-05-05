namespace delivery_server_api.Models.FoodModels
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }

        public FoodViewModel(Guid Id, string Title, string Price) {
            this.Id = Id;
            this.Title = Title;
            this.Price = Price;
        }
    }
}
