using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL
{
    public class AppRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
