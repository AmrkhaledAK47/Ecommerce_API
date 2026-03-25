namespace ECommerce.BLL
{
    public class UserRoleDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<RoleAssignmentDTO> Roles { get; set; } = [];
    }

    public class RoleAssignmentDTO
    {
        public string RoleName { get; set; }
        public bool IsAssigned { get; set; }
    }
}
