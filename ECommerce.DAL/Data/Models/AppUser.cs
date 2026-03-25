using Microsoft.AspNetCore.Identity;

namespace ECommerce.DAL
{
    public class AppUser : IdentityUser
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }

    }
}
