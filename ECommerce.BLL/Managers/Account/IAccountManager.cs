using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using System.Timers;

namespace ECommerce.BLL
{
    public interface IAccountManager
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO model);
        Task<SignInResult> LoginAsync(LoginDTO model, AppUser user);
        Task LogoutAsync();
        Task<AppUser>? FindByEmailAsync(string email);
        Task<IdentityResult> AddRoleToUserAsync(string userId, string roleName);
    }
}
