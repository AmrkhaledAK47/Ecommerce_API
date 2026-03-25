using System.ComponentModel.DataAnnotations;

namespace ECommerce.BLL
{
    public class ProductCreateDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? ExpiryDate { get; set; }

        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
