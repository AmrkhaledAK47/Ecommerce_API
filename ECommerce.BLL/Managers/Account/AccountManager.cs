using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.BLL
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AccountManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterDTO model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            return await _userManager.CreateAsync(user, model.Password);
        }
        
        public async Task<SignInResult> LoginAsync(LoginDTO model, AppUser user)
        {
            return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
        }
        public async Task<AppUser>? FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> AddRoleToUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            if (!await _roleManager.RoleExistsAsync(roleName)) return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            return await _userManager.AddToRoleAsync(user, roleName);
        }

    }
}
