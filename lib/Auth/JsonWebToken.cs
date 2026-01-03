using System;
using System.Collections.Generic;
namespace Auth
{
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
        public Guid DefaultRoleID { get; set; }
        public string Username { get; set; }
        public string EmployeeNo { get; set; }
        public List<string> APPPermission { get; set; }
    }

    public class ClientJsonWebToken
    {
        public string token { get; set; }
        public long expires { get; set; }
        public long expires_in { get; set; }
        public string display_name { get; set; }
    }
}
