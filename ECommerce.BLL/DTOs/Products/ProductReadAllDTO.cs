namespace ECommerce.BLL
{
    public class ProductReadAllDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
}
