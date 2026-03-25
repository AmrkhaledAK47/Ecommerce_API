namespace ECommerce.Common
{
    public class ProductFilterParameters : BaseFilterParameters
    {
        public int? MinCount { get; set; }
        public int? MaxCount { get; set; }
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
    }
}
