using System;
namespace Auth_User.Params.Inputs
{
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
