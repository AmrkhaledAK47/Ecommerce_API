namespace ECommerce.DAL
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public string? ImageUrl { get; set; }
        //ExpriyDate validation : must be greater than or equal to the current date
        public DateOnly? ExpiryDate { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime? UpdatedAt { get; set; }
    }
}
