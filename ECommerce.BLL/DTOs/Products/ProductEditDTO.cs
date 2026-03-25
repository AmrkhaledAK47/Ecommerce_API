using System.ComponentModel.DataAnnotations;
namespace ECommerce.BLL
{
    public class ProductEditDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [Range(1, 5000, ErrorMessage = "Price must be between 1 and 5000.")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Count must be greater than 0")]
        public int Count { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly? ExpiryDate { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public string? CurrentImageUrl { get; set; }

    }
}
