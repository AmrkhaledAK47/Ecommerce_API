using ECommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.BLL
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleManagerService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<RoleDTO>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description
            });
        }
        public async Task<RoleDTO?> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return null;
            return new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
        public async Task<IdentityResult> CreateRoleAsync(RoleCreateDTO model)
        {
            var role = new AppRole
            {
                Name = model.Name,
                Description = model.Description
            };
            return await _roleManager.CreateAsync(role);
        }
        public async Task<IdentityResult> EditRoleAsync(RoleDTO model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null) return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            role.Name = model.Name;
            role.Description = model.Description;
            return await _roleManager.UpdateAsync(role);
        }
        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return IdentityResult.Failed(new IdentityError { Description = "Role not found" });
            return await _roleManager.DeleteAsync(role);
        }
    }
}
