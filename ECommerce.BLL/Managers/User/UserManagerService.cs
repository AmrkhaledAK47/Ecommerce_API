using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BLL
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserManagerService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Roles = roles.ToList()
                });
            }

            return userDTOs;
        }

        public async Task<UserRoleDTO?> GetUserWithRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = await _roleManager.Roles.Select(r => r.Name!).ToListAsync();

            return new UserRoleDTO
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                Email = user.Email!,
                Roles = allRoles.Select(role => new RoleAssignmentDTO
                {
                    RoleName = role,
                    IsAssigned = userRoles.Contains(role)
                }).ToList()
            };
        }

        public async Task<IdentityResult> UpdateUserRolesAsync(string userId, List<RoleAssignmentDTO> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var currentRoles = await _userManager.GetRolesAsync(user);

            var rolesToAdd = roles
                .Where(r => r.IsAssigned && !currentRoles.Contains(r.RoleName))
                .Select(r => r.RoleName);

            var rolesToRemove = roles
                .Where(r => !r.IsAssigned && currentRoles.Contains(r.RoleName))
                .Select(r => r.RoleName);

            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded) return addResult;

            return await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        }
    }
}
