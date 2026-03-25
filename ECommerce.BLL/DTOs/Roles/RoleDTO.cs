using System.ComponentModel.DataAnnotations;

namespace ECommerce.BLL
{
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}