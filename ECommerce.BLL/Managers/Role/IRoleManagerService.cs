using Microsoft.AspNetCore.Identity;

namespace ECommerce.BLL
{
    public interface IRoleManagerService
    {
        Task<IEnumerable<RoleDTO>> GetAllRolesAsync();
        Task<RoleDTO?> GetRoleByIdAsync(string id);
        Task<IdentityResult> CreateRoleAsync(RoleCreateDTO model);
        Task<IdentityResult> EditRoleAsync(RoleDTO model);
        Task<IdentityResult> DeleteRoleAsync(string id);
    }
}