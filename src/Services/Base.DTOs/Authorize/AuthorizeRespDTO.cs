using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Base.DTOs
{
    internal class AuthorizeRespDTO
    {

    }

    public partial class AuthorizeDataResp
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public JsonWebToken data { get; set; } = null!;
    }

    public partial class UserTokenResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public UserTokenResultReturnObject data { get; set; }
    }

    public class UserTokenResultReturnObject
    {
        public Guid? userGUID { get; set; }
        public string userName { get; set; }
        public string empCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string positionCode { get; set; }
        public string position { get; set; }
        public string department { get; set; }
        public string divisionCode { get; set; }
        public string division { get; set; }
        public string email { get; set; }
        public string leaderCode { get; set; }
    }

    public class UserRoleResultReturn
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string EmployeeNo { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public Guid? DefaultRoleID { get; set; }
    }

    public class UserResult
    {
        public Guid? ID { get; set; }
        public string EmployeeNo { get; set; }
    }

    public class JsonWebToken
    {
        public string token { get; set; }
        public long expires { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
        public Guid user_id { get; set; }
        public string display_name { get; set; }
        public string Auth_UserID { get; set; }
        public string DefaultAPP { get; set; }
        public string Username { get; set; }
        public string EmployeeNo { get; set; }
    }

    public class LoginParam
    {
        /// <summary>
        /// password, refresh_token
        /// </summary>
        public string grant_type { get; set; }
        /// <summary>
        /// ชื่อผู้ใช้
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// รหัสผ่าน
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// refresh token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// web ที่ Login
        /// </summary>
        public string web_base { get; set; }

        /// <summary>
        /// Login มาจาก intranet
        /// </summary>
        public string fromIntranet { get; set; }
    }
}
