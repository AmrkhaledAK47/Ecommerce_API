using System.ComponentModel.DataAnnotations;

namespace ECommerce.BLL
{
    public class RoleCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
