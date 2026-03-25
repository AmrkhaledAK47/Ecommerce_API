using Microsoft.AspNetCore.Identity;

namespace ECommerce.BLL
{
    public interface IUserManagerService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserRoleDTO?> GetUserWithRolesAsync(string userId);
        Task<IdentityResult> UpdateUserRolesAsync(string userId, List<RoleAssignmentDTO> roles);
    }
}
