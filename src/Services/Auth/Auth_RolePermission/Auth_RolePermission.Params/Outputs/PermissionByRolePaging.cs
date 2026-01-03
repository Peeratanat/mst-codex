using Base.DTOs.IDT;
using PagingExtensions;

namespace Auth_RolePermission.Params.Outputs
{
    public class PermissionByRolePaging
    {
        public PermissionByRoleDTO PermissionByRole { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
