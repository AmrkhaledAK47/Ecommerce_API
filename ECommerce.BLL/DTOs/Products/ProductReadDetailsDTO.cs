namespace ECommerce.BLL
{
    public class ProductReadDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string CategoryName { get; set; }

        public DateOnly? ExpiryDate { get; set; }

    }
}
