using System.Text.Json.Serialization;

namespace ECommerce.BLL
{
    public class CategoryReadDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<ProductReadDetailsDTO>? Products { get; set; }
    }
}
