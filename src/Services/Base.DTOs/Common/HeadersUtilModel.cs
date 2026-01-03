using Base.DTOs.SystemMessage;
using Newtonsoft.Json;

namespace Base.DTOs.Common
{
    public class UserWithMainRoleModel
    {
        public string EmpCode { get; set; }
        public string MainAppRoleCode { get; set; }
        public string RoleCategoryCode { get; set; }
        public string PositionName { get; set; }
        public string Email { get; set; }
    }
}